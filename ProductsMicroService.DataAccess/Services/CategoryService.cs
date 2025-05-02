using FluentValidation;
using ProductsMicroService.BusinessLogic.Dtos;
using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Exceptions;
using ProductsMicroService.BusinessLogic.Interfaces;
using ProductsMicroService.BusinessLogic.ServiceContracts;
using ProductsMicroService.BusinessLogic.Specifications;
using ValidationException = ProductsMicroService.BusinessLogic.Exceptions.ValidationException;

namespace ProductsMicroService.DataAccess.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CategoryCreateDto> _createValidator;
    private readonly IValidator<CategoryUpdateDto> _updateValidator;

    public CategoryService(
        IUnitOfWork unitOfWork,
        IValidator<CategoryCreateDto> createValidator,
        IValidator<CategoryUpdateDto> updateValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<Guid> CreateAsync(CategoryCreateDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );
            
            throw new ValidationException(errors);
        }

        var category = new Category
        {
            Name = dto.Name
        };

        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.CommitAsync();

        return category.Id;
    }

    public async Task<GetByIdCategoryDto?> GetByIdAsync(Guid id)
    {
        var spec = new CategoryByIdSpecification(id);
        return await _unitOfWork.Categories.GetAsync(spec);
    }

    public async Task<IReadOnlyList<ListCategoryDto>> ListAsync(string? search = null)
    {
        var spec = new CategoryListSpecification(search);
        return await _unitOfWork.Categories.ListAsync(spec);
    }

    public async Task<GetByIdCategoryDto?> UpdateAsync(CategoryUpdateDto dto)
    {
        var validationResult = await _updateValidator.ValidateAsync(dto);
        
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );
            
            throw new ValidationException(errors);
        }

        var category = new Category
        {
            Id = dto.Id,
            Name = dto.Name
        };

        await _unitOfWork.Categories.UpdateAsync(category);
        await _unitOfWork.CommitAsync();

        return await GetByIdAsync(dto.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _unitOfWork.Categories.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
        return true;
    }
}