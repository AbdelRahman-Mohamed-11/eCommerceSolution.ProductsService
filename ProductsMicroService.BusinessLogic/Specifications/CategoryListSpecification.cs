using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Interfaces.Specifications;
using ProductsMicroService.BusinessLogic.Dtos;
using System.Linq.Expressions;

namespace ProductsMicroService.BusinessLogic.Specifications;

public class CategoryListSpecification : ISpecification<Category, ListCategoryDto>
{
    private readonly string? _searchTerm;

    public CategoryListSpecification(string? searchTerm = null)
    {
        _searchTerm = searchTerm;
    }

    public Expression<Func<Category, bool>>? Criteria => string.IsNullOrEmpty(_searchTerm) 
        ? null 
        : c => c.Name.Contains(_searchTerm);
    public List<Expression<Func<Category, object>>> Includes { get; } = new();
    public Expression<Func<Category, object>>? OrderByAsc => c => c.Name;
    public Expression<Func<Category, object>>? OrderByDesc => null;
    public Expression<Func<Category, ListCategoryDto>> Selector => c => new ListCategoryDto(c.Id, c.Name);
}