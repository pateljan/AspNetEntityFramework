using EFCoreAspNet.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAspNet
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Person> People { get; set; }
    }
}
