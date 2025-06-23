using Microsoft.EntityFrameworkCore;
using PPM.Models;

namespace PPM
{
    public class PPMDBContext : DbContext //Note: UserManager<User> or IdentityUser is a library that should be used in a future project one day
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Park> Parks { get; set; } = null!;
        public DbSet<Event> Event { get; set; } = null!;
        public DbSet<Registration> Registration { get; set; } = null!;

        public PPMDBContext() : base() { }
        public PPMDBContext(DbContextOptions options) : base(options) { }
    }
}
