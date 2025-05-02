using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Interfaces.Specifications;
using ProductsMicroService.BusinessLogic.Dtos;
using System.Linq.Expressions;

namespace ProductsMicroService.BusinessLogic.Specifications;

public class ProductListSpecification : ISpecification<Product, ListProductDto>
{
    private readonly string? _searchTerm;

    public ProductListSpecification(string? searchTerm = null)
    {
        _searchTerm = searchTerm;
        Includes.Add(p => p.Category);
    }

    public Expression<Func<Product, bool>>? Criteria => string.IsNullOrEmpty(_searchTerm) 
        ? null 
        : p => p.Name.Contains(_searchTerm);
    public List<Expression<Func<Product, object>>> Includes { get; } = new();
    public Expression<Func<Product, object>>? OrderByAsc => p => p.Name;
    public Expression<Func<Product, object>>? OrderByDesc => null;
    public Expression<Func<Product, ListProductDto>> Selector => p => new ListProductDto(
        p.Id, p.Name, p.Category.Name, p.UnitPrice, p.QuantityInStock);
}