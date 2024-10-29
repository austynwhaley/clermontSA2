using Microsoft.EntityFrameworkCore;

public class ClermontDb : DbContext
{
    public ClermontDb(DbContextOptions<ClermontDb> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
