using System.Linq;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace ShortcutManager.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ShortcutItemEntity> Shortcuts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=ShortcutsDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortcutItemEntity>().ToTable("Shortcuts");
        }

        public int GetNextShortcutId()
        {
            var lastExistingId = Shortcuts.Select(i => i.Id).ToList().OrderByDescending(i => i).FirstOrDefault();

            return lastExistingId+1;
        }
    }
}