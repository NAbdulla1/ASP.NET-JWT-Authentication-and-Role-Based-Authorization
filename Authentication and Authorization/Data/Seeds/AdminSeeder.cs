using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Data.Seeds
{
    public static class AdminSeeder
    {
        public static void Initialize(IServiceProvider services)
        {
            using (var context = new UserAccountContext(
                services.GetRequiredService<DbContextOptions<UserAccountContext>>()))
            {
                if (context.Users.Where(user => user.UserType == Entities.UserType.Admin).Any())
                {
                    return;
                }

                context.Users.Add(new Entities.User
                {
                    Email = "admin@email.com",
                    Password = "1234",
                    UserType = Entities.UserType.Admin
                });

                context.SaveChanges();
            }
        }
    }
}
