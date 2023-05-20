using Microsoft.EntityFrameworkCore;
using TestTaskMajor.Models.Domain;

namespace TestTaskMajor.Data
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Item> Items { get; set; }

    }
}
