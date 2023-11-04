using Authentication_and_Authorization.Data.Entities;

namespace Authentication_and_Authorization.Data.DTOs
{
    public class QueryResult<TEntity> where TEntity : EntityBase
    {
        public int Total { get; }
        public IEnumerable<TEntity> Entities { get; }

        public QueryResult(IEnumerable<TEntity> entities, int total)
        {
            Entities = entities;
            Total = total;
        }
    }
}
