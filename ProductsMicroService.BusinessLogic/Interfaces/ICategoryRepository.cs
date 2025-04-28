using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Interfaces.Specifications;

namespace ProductsMicroService.BusinessLogic.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetAsync(ISpecification<Category> spec);
    Task<IReadOnlyList<Category>> ListAsync(ISpecification<Category> spec);
    Task<TResult?> GetAsync<TResult>(ISpecification<Category, TResult> spec);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<Category, TResult> spec);
    Task AddAsync(Category entity);
    Task UpdateAsync(Category entity);
    Task DeleteAsync(Guid id);
}