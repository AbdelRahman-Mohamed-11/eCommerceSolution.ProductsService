using ProductsMicroService.BusinessLogic.Dtos;

namespace ProductsMicroService.BusinessLogic.ServiceContracts;

public interface ICategoryService
{
    Task<Guid> CreateAsync(CategoryCreateDto dto);
    Task<GetByIdCategoryDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<ListCategoryDto>> ListAsync(string? search = null);
    Task<GetByIdCategoryDto?> UpdateAsync(CategoryUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}