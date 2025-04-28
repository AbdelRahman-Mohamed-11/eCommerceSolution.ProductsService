using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Interfaces.Specifications;

namespace ProductsMicroService.BusinessLogic.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetAsync(ISpecification<Product> spec);
    Task<IReadOnlyList<Product>> ListAsync(ISpecification<Product> spec);

    Task<TResult?> GetAsync<TResult>(ISpecification<Product, TResult> spec);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<Product, TResult> spec);
    
    Task AddAsync(Product entity);
    Task UpdateAsync(Product entity);
    Task DeleteAsync(Guid id);
}