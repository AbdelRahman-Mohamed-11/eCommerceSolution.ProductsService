using System.Linq.Expressions;

namespace ProductsMicroService.BusinessLogic.Interfaces.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>>? OrderByAsc { get; }
    Expression<Func<T, object>>? OrderByDesc { get; }
}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>> Selector { get; }
}