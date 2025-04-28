using ProductsMicroService.BusinessLogic.Dtos;

namespace ProductsMicroService.BusinessLogic.ServiceContracts;

public interface IProductService
{
    Task<Guid> CreateAsync(ProductCreateDto dto);
    Task<GetByIdProductDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<ListProductDto>> ListAsync();
    Task<GetByIdProductDto?> UpdateAsync(ProductUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}
