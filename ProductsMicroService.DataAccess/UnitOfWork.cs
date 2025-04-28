using ProductsMicroService.BusinessLogic.Interfaces;
using ProductsMicroService.DataAccess.Context;

namespace ProductsMicroService.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductsDbContext _dbContext;

    public UnitOfWork(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
        Products = new ProductRepository(_dbContext);
        Categories = new CategoryRepository(_dbContext);
    }

    public IProductRepository Products { get; }
    public ICategoryRepository Categories { get; }

    public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();

    public void Dispose() => _dbContext.Dispose();
}