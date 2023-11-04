using Authentication_and_Authorization.Data.Entities;

namespace Authentication_and_Authorization.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> HasAnyAdmin();
    }
}
