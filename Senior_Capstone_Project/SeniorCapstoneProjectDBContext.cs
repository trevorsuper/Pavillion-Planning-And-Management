using Microsoft.EntityFrameworkCore;
using Senior_Capstone_Project.Models;

namespace Senior_Capstone_Project
{
    public class SeniorCapstoneProjectDBContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public SeniorCapstoneProjectDBContext() : base() { }
        public SeniorCapstoneProjectDBContext(DbContextOptions options) : base(options){ }
    }
}
