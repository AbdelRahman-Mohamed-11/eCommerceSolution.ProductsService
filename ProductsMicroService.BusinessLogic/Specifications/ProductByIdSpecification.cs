using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Interfaces.Specifications;
using ProductsMicroService.BusinessLogic.Dtos;
using System.Linq.Expressions;

namespace ProductsMicroService.BusinessLogic.Specifications;

public class ProductByIdSpecification : ISpecification<Product, GetByIdProductDto>
{
    private readonly Guid _id;

    public ProductByIdSpecification(Guid id)
    {
        _id = id;
        Includes.Add(p => p.Category);
    }

    public Expression<Func<Product, bool>>? Criteria => p => p.Id == _id;
    public List<Expression<Func<Product, object>>> Includes { get; } = new();
    public Expression<Func<Product, object>>? OrderByAsc => null;
    public Expression<Func<Product, object>>? OrderByDesc => null;
    public Expression<Func<Product, GetByIdProductDto>> Selector => p => new GetByIdProductDto(
        p.Id, p.Name, p.CategoryId, p.Category.Name, p.UnitPrice, p.QuantityInStock);
}