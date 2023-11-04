using Authentication_and_Authorization.Data.Entities;
using System.Linq.Expressions;

namespace Authentication_and_Authorization.Data.DTOs
{
    public class QueryDetails<TEntity> where TEntity : EntityBase
    {
        public IEnumerable<Expression<Func<TEntity, bool>>> WherePredicates = new List<Expression<Func<TEntity, bool>>>();
        public Expression<Func<TEntity, object>> OrderExpr = entity => entity.Id;
        public bool OrderDescending = false;
    }
}
