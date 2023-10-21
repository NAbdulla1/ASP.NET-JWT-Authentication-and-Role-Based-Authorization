using Authentication_and_Authorization.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
    }
}
