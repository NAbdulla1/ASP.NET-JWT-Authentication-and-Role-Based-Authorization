using StackExchange.Redis;
namespace Authentication_and_Authorization.Data.InMemory
{
    public interface IInMemoryDB : IDisposable
    {
        IDatabase GetDb();
    }
}
