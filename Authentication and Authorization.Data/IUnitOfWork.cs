using Authentication_and_Authorization.Data.Repositories;

namespace Authentication_and_Authorization.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        Task<int> CommitAsync();
    }
}
