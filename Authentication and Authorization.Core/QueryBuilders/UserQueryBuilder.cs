using Authentication_and_Authorization.Core.DTOs;
using Authentication_and_Authorization.Data.Entities;
using Authentication_and_Authorization.Data.Query;

namespace Authentication_and_Authorization.Core.QueryBuilders
{
    public interface IUserQueryBuilder
    {
        QueryDetails<User> BuildGetAllQuery(IndexDTO indexData, string? searchTerm);
    }

    public class UserQueryBuilder : IUserQueryBuilder
    {
        public QueryDetails<User> BuildGetAllQuery(IndexDTO indexData, string? searchTerm)
        {
            var queryDetails = new QueryDetails<User>(new UserOrderByStrategy(), indexData.SortBy, indexData.SortOrder);

            AddSearchPredicate(queryDetails, searchTerm);

            return queryDetails;
        }

        private static void AddSearchPredicate(QueryDetails<User> queryDetails, string? searchTerm)
        {
            if (searchTerm == null)
            {
                return;
            }

            queryDetails.AddPredicate(user => user.Email.ToLower().Contains(searchTerm.ToLower()));
        }
    }
}
