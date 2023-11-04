using Authentication_and_Authorization.Data.Repositories;

namespace Authentication_and_Authorization.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserAccountContext _userAccountContext;

        public UnitOfWork(UserAccountContext userAccountContext)
        {
            _userAccountContext = userAccountContext;
            InitializeRepositories(userAccountContext);
        }

        private void InitializeRepositories(UserAccountContext userAccountContext)
        {
            Users = new UserRepository(userAccountContext);
        }

        public IUserRepository Users { get; private set; }

        public async Task<int> CommitAsync()
        {
            return await _userAccountContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _userAccountContext.Dispose();
        }
    }
}
