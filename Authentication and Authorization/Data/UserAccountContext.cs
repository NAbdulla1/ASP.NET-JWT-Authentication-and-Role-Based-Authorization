using Authentication_and_Authorization.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Data
{
    public class UserAccountContext : DbContext
    {
        public UserAccountContext(DbContextOptions<UserAccountContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
