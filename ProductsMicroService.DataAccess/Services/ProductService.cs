using ProductsMicroService.BusinessLogic.Dtos;
using ProductsMicroService.BusinessLogic.ServiceContracts;

namespace ProductsMicroService.DataAccess.Services;

public class ProductService : IProductService
{
    public Task<Guid> CreateAsync(ProductCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<GetByIdProductDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<ListProductDto>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetByIdProductDto?> UpdateAsync(ProductUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}