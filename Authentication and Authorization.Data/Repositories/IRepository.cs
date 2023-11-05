using Authentication_and_Authorization.Data.Query;
using Authentication_and_Authorization.Data.Entities;

namespace Authentication_and_Authorization.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity?> GetOneAsync(long id);
        Task<QueryResult<TEntity>> GetAllAsync(QueryDetails<TEntity> queryDetails, int pageIndex, int pageSize);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
