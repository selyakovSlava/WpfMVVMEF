using Microsoft.EntityFrameworkCore;
using WPFExampleEF.Models;

namespace WPFExampleEF.Classes
{
    public class ApplicationContext : DbContext
    {
        public DbSet<BondModel> Bonds { get; set; } = null!;

        public ApplicationContext()
        {
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WPFExampleEF_1.db");
        }
    }
}
