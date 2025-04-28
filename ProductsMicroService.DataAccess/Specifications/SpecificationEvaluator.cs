using Microsoft.EntityFrameworkCore;
using ProductsMicroService.BusinessLogic.Interfaces.Specifications;

namespace ProductsMicroService.DataAccess.Specifications;


    public static class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (spec.OrderByAsc != null)
                query = query.OrderBy(spec.OrderByAsc);

            if (spec.OrderByDesc != null)
                query = query.OrderByDescending(spec.OrderByDesc);

            return query;
        }
    }