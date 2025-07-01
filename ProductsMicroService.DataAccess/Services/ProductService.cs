using FluentValidation;
using ProductsMicroService.BusinessLogic.common;
using ProductsMicroService.BusinessLogic.Dtos;
using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Interfaces;
using ProductsMicroService.BusinessLogic.ServiceContracts;
using ProductsMicroService.BusinessLogic.Specifications;

namespace ProductsMicroService.DataAccess.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ProductCreateDto> _createValidator;
    private readonly IValidator<ProductUpdateDto> _updateValidator;

    public ProductService(
        IUnitOfWork unitOfWork,
        IValidator<ProductCreateDto> createValidator,
        IValidator<ProductUpdateDto> updateValidator)
    {
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<Result<Guid>> CreateAsync(ProductCreateDto dto)
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

            return Result<Guid>.Invalid(errors);
        }

        var category = await _unitOfWork.Categories.GetAsync(new CategoryByIdSpecification(dto.CategoryId));

        if (category is null)
            return Result<Guid>.Failure("Category not found", 404);

        var product = new Product
        {
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            UnitPrice = dto.UnitPrice,
            QuantityInStock = dto.QuantityInStock
        };

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.CommitAsync();

        return Result<Guid>.Created(product.Id);
    }

    public async Task<Result<GetByIdProductDto?>> GetByIdAsync(Guid id)
    {
        var spec = new ProductByIdSpecification(id);
        var product = await _unitOfWork.Products.GetAsync(spec);
        
        return product is null 
            ? Result<GetByIdProductDto?>.Failure("Product not found", 404)
            : Result<GetByIdProductDto?>.Success(product);
    }

    public async Task<Result<IReadOnlyList<ListProductDto>>> ListAsync(string? search)
    {
        var spec = new ProductListSpecification(search);
        var products = await _unitOfWork.Products.ListAsync(spec);
        return Result<IReadOnlyList<ListProductDto>>.Success(products);
    }

    public async Task<Result<GetByIdProductDto?>> UpdateAsync(ProductUpdateDto dto)
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

            return Result<GetByIdProductDto?>.Invalid(errors);
        }

        var existingProduct = await _unitOfWork.Products.GetAsync(new ProductByIdSpecification(dto.Id));
        if (existingProduct is null)
            return Result<GetByIdProductDto?>.Failure("Product not found", 404);

        var category = await _unitOfWork.Categories.GetAsync(new CategoryByIdSpecification(dto.CategoryId));
        if (category is null)
            return Result<GetByIdProductDto?>.Failure("Category not found", 404);

        var product = new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            UnitPrice = dto.UnitPrice,
            QuantityInStock = dto.QuantityInStock
        };

        await _unitOfWork.Products.UpdateAsync(product);
        await _unitOfWork.CommitAsync();

        var updatedProduct = await _unitOfWork.Products.GetAsync(new ProductByIdSpecification(dto.Id));
        return updatedProduct is not null
            ? Result<GetByIdProductDto?>.Success(updatedProduct)
            : Result<GetByIdProductDto?>.Failure("Error retrieving updated product", 500);
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        var product = await _unitOfWork.Products.GetAsync(new ProductByIdSpecification(id));
        if (product is null)
            return Result<bool>.Failure("Product not found", 404);

        await _unitOfWork.Products.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
        
        return Result<bool>.Success(true);
    }
}
