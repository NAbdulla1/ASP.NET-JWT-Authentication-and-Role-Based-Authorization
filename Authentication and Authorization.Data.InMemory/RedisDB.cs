using Authentication_and_Authorization.Data.InMemory.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Authentication_and_Authorization.Data.InMemory
{
    public class RedisDB : IInMemoryDB
    {
        private readonly ILogger<RedisDB> _logger;
        private readonly ConnectionMultiplexer _redisConnection;

        public RedisDB(IConfiguration configuration, ILogger<RedisDB> logger)
        {
            var redisConfig =
                configuration.GetSection(RedisConfig.SectionName).Get<RedisConfig>() ??
                throw new InvalidOperationException($"Redis configuration is not available at '{RedisConfig.SectionName}' section.");
            _logger = logger;

            _logger.LogInformation("Creating redis connection");
            _redisConnection = ConnectionMultiplexer.Connect($"{redisConfig.Host}:{redisConfig.Port}");
        }

        public IDatabase GetDb()
        {
            return _redisConnection.GetDatabase();
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing redis connection.");
            _redisConnection.Dispose();
        }
    }
}
