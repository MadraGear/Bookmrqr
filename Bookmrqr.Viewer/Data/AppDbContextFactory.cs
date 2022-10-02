using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bookmrqr.Viewer.Data
{
    public class AppDbContextFactory : IAppDbContextFactory
    {
        public AppDbContext Create()
        {
            // Create a configuration 
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), 
            //     "BookmrqrData/bookmrqr.db");
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "bookmrqr.db");
            optionsBuilder.UseSqlite("Data Source=" + dbPath);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}