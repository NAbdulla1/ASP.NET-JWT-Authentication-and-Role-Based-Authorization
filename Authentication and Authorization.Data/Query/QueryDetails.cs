using Authentication_and_Authorization.Data.Entities;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace Authentication_and_Authorization.Data.Query
{
    public class QueryDetails<TEntity> where TEntity : EntityBase
    {
        private ICollection<Expression<Func<TEntity, bool>>> _wherePredicates;

        public ICollection<Expression<Func<TEntity, bool>>> WherePredicates { get { return _wherePredicates.ToImmutableList(); } private set { _wherePredicates = value; } }
        public Expression<Func<TEntity, object>> OrderExpr { get; private set; }
        public bool OrderDescending { get; private set; }

        public QueryDetails(IOrderByStragegy<TEntity> orderByStrategy, string? sortBy, string? sortOrder)
        {
            _wherePredicates = new List<Expression<Func<TEntity, bool>>>();
            OrderExpr = orderByStrategy.GetExpr(sortBy);
            OrderDescending = sortOrder == "desc";
        }

        public void AddPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            _wherePredicates.Add(predicate);
        }
    }
}
