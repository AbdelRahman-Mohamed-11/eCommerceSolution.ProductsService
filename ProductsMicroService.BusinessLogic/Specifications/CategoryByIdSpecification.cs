using ProductsMicroService.BusinessLogic.Entities;
using ProductsMicroService.BusinessLogic.Interfaces.Specifications;
using ProductsMicroService.BusinessLogic.Dtos;
using System.Linq.Expressions;

namespace ProductsMicroService.BusinessLogic.Specifications;

public class CategoryByIdSpecification : ISpecification<Category, GetByIdCategoryDto>
{
    private readonly Guid _id;

    public CategoryByIdSpecification(Guid id)
    {
        _id = id;
    }

    public Expression<Func<Category, bool>>? Criteria => c => c.Id == _id;
    public List<Expression<Func<Category, object>>> Includes { get; } = new();
    public Expression<Func<Category, object>>? OrderByAsc => null;
    public Expression<Func<Category, object>>? OrderByDesc => null;
    public Expression<Func<Category, GetByIdCategoryDto>> Selector => c => new GetByIdCategoryDto(c.Id, c.Name);
}