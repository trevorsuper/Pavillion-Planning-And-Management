using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PPM.Models;

namespace PPM
{
    public class PPMDBContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public PPMDBContext() : base() { }
        public PPMDBContext(DbContextOptions options) : base(options) { }
    }
}
