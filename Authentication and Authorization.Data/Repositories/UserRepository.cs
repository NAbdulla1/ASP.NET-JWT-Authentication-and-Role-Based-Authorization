using Authentication_and_Authorization.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserAccountContext dbContext) : base(dbContext) { }

        public async Task<bool> HasAnyAdmin()
        {
            return await Users.AnyAsync(user => user.UserType == UserType.Admin);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public DbSet<User> Users => ((UserAccountContext)_dbContext).Users;
    }
}
