using Microsoft.EntityFrameworkCore;
using PPM.Models;

namespace PPM
{
    public class PPMDBContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Park> Parks { get; set; } = null!;
        public DbSet<Event> Event { get; set; } = null!;
        public DbSet<Registration> Registration { get; set; } = null!;

        public PPMDBContext() : base() { }
        public PPMDBContext(DbContextOptions options) : base(options) { }
    }
}
