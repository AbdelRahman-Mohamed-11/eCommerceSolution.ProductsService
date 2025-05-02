using ProductsMicroService.BusinessLogic.common;
using ProductsMicroService.BusinessLogic.Dtos;

namespace ProductsMicroService.BusinessLogic.ServiceContracts;

public interface IProductService
{
    Task<Result<Guid>> CreateAsync(ProductCreateDto dto);
    Task<Result<GetByIdProductDto?>> GetByIdAsync(Guid id);
    Task<Result<IReadOnlyList<ListProductDto>>> ListAsync();
    Task<Result<GetByIdProductDto?>> UpdateAsync(ProductUpdateDto dto);
    Task<Result<bool>> DeleteAsync(Guid id);
}
