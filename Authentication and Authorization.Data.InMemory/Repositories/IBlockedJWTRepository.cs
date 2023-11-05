using StackExchange.Redis;

namespace Authentication_and_Authorization.Data.InMemory.Repositories
{
    public interface IBlockedJWTRepository
    {
        Task<bool> BlockJWT(string jwt, double stillValidInSeconds);
        Task<bool> IsJWTBlocked(string jwt);
    }

    public class BlockedJWTRepository : IBlockedJWTRepository
    {
        private IDatabase _redisDb;

        public BlockedJWTRepository(IInMemoryDB memoryDb)
        {
            _redisDb = memoryDb.GetDb();
        }

        public async Task<bool> BlockJWT(string jwt, double stillValidInSeconds)
        {
            return await _redisDb.StringSetAsync(jwt, true, TimeSpan.FromSeconds(stillValidInSeconds));
        }

        public async Task<bool> IsJWTBlocked(string jwt)
        {
            return await _redisDb.KeyExistsAsync(jwt);
        }
    }
}
