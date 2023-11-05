using Authentication_and_Authorization.Data.Query;
using Authentication_and_Authorization.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public async Task<QueryResult<TEntity>> GetAllAsync(QueryDetails<TEntity> queryDetails, int pageIndex, int pageSize)
        {
            var query = queryDetails.WherePredicates.Aggregate(
                _dbContext.Set<TEntity>().AsQueryable(),
                (current, predicate) => current.Where(predicate));

            query = queryDetails.OrderDescending ? query.OrderByDescending(queryDetails.OrderExpr) : query.OrderBy(queryDetails.OrderExpr);

            var total = await query.CountAsync();
            var paged = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();

            return new QueryResult<TEntity>(paged, total);
        }

        public async Task<TEntity?> GetOneAsync(long id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
