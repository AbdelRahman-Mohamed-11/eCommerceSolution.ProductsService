namespace ProductsMicroService.BusinessLogic.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    Task<int> CommitAsync();
}