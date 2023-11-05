using Authentication_and_Authorization.Data.Entities;
using System.Linq.Expressions;

namespace Authentication_and_Authorization.Data.Query
{
    public interface IOrderByStragegy<TEntity> where TEntity : EntityBase
    {
        Expression<Func<TEntity, object>> GetExpr(string? sortBy);
    }
}
