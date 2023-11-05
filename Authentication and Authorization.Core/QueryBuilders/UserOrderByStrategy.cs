using Authentication_and_Authorization.Data.Query;
using Authentication_and_Authorization.Data.Entities;
using System.Linq.Expressions;

namespace Authentication_and_Authorization.Core.QueryBuilders
{
    public class UserOrderByStrategy : IOrderByStragegy<User>
    {
        public Expression<Func<User, object>> GetExpr(string? sortBy)
        {
            return sortBy?.ToLower() switch
            {
                "email" => user => user.Email,
                "usertype" => user => user.UserType,
                _ => user => user.Id,
            };
        }
    }
}
