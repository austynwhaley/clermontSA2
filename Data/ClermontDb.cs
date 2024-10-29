using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ClermontDb : DbContext
{
    public ClermontDb(DbContextOptions<ClermontDb> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public async Task SeedDataAsync(RandomUser randomUserService)
    {
        if (!Users.Any())
        {
            for (int i = 0; i < 20; i++)
            {
                var randomUserData = await randomUserService.FetchRandomUserDataAsync();
                if (randomUserData != null)
                {
                    var user = new User
                    {
                        Name = randomUserData.Name,
                        Email = randomUserData.Email,
                        Phone = randomUserData.Phone,
                        Address = randomUserData.Address,
                        PictureUrl = randomUserData.PictureUrl
                    };
                    
                    Users.Add(user);
                }
            }
            await SaveChangesAsync();
        }
    }
}
