using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmrqr.EventProcessor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bookmrqr.db");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
    }
}