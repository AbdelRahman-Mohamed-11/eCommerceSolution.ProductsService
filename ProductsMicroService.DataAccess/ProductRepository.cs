using Microsoft.EntityFrameworkCore;
using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Interfaces;
using ProductsMicroService.BusinessLogic.Interfaces.Specifications;
using ProductsMicroService.DataAccess.Context;
using ProductsMicroService.DataAccess.Specifications;

namespace ProductsMicroService.DataAccess;

public class ProductRepository(ProductsDbContext dbContext) : IProductRepository
{
    public async Task<Product?> GetAsync(ISpecification<Product> spec)
    {
        var query = SpecificationEvaluator<Product>.GetQuery(dbContext.Products.AsQueryable(), spec);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<Product>> ListAsync(ISpecification<Product> spec)
    {
        var query = SpecificationEvaluator<Product>.GetQuery(dbContext.Products.AsQueryable(), spec);
        return await query.ToListAsync();
    }

    public async Task<TResult?> GetAsync<TResult>(ISpecification<Product, TResult> spec)
    {
        var query = SpecificationEvaluator<Product>.GetQuery(dbContext.Products.AsQueryable(), spec);
        return await query.Select(spec.Selector).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<Product, TResult> spec)
    {
        var query = SpecificationEvaluator<Product>.GetQuery(dbContext.Products.AsQueryable(), spec);
        return await query.Select(spec.Selector).ToListAsync();
    }
    public async Task AddAsync(Product entity)
    {
        dbContext.Products.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product entity)
    {
        dbContext.Products.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (entity != null)
        {
            dbContext.Products.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
    
}