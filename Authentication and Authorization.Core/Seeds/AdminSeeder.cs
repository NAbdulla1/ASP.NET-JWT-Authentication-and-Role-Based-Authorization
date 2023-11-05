using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication_and_Authorization.Core.Seeds
{
    public static class AdminSeeder
    {
        public static async Task Initialize(IServiceProvider services)
        {
            using (var unitOfWork = services.GetRequiredService<IUnitOfWork>())
            {
                if (await unitOfWork.Users.HasAnyAdmin())
                {
                    return;
                }

                unitOfWork.Users.Add(new User
                {
                    Email = "admin@email.com",
                    Password = "1234",
                    UserType = UserType.Admin
                });

                await unitOfWork.CommitAsync();
            }
        }
    }
}
