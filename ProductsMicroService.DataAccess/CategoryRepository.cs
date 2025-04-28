using Microsoft.EntityFrameworkCore;
using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Interfaces;
using ProductsMicroService.BusinessLogic.Interfaces.Specifications;
using ProductsMicroService.DataAccess.Context;
using ProductsMicroService.DataAccess.Specifications;

namespace ProductsMicroService.DataAccess;

public class CategoryRepository(ProductsDbContext dbContext) : ICategoryRepository
{
    public async Task<Category?> GetAsync(ISpecification<Category> spec)
    {
        var query = SpecificationEvaluator<Category>.GetQuery(dbContext.Categories.AsQueryable(), spec);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<Category>> ListAsync(ISpecification<Category> spec)
    {
        var query = SpecificationEvaluator<Category>.GetQuery(dbContext.Categories.AsQueryable(), spec);
        return await query.ToListAsync();
    }

    public async Task<TResult?> GetAsync<TResult>(ISpecification<Category, TResult> spec)
    {
        var query = SpecificationEvaluator<Category>.GetQuery(dbContext.Categories.AsQueryable(), spec);
        return await query.Select(spec.Selector).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<Category, TResult> spec)
    {
        var query = SpecificationEvaluator<Category>.GetQuery(dbContext.Categories.AsQueryable(), spec);
        return await query.Select(spec.Selector).ToListAsync();
    }

    public async Task AddAsync(Category entity)
    {
        dbContext.Categories.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        dbContext.Categories.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await dbContext.Categories.FindAsync(id);
        if (entity != null)
        {
            dbContext.Categories.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}