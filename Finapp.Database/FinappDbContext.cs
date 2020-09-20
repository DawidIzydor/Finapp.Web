using Microsoft.EntityFrameworkCore;

namespace Finapp.Database
{
    public class FinappDbContext : DbContext
    {
        public DbSet<FinappUser> Users { get; set; }
        public DbSet<FinancialStatus> FinancialStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=localhost\\sqlexpress;Initial Catalog=FinApp;Integrated Security=True",
                opt => opt.MigrationsHistoryTable("EFFinappMigrations", "fip"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("fip");

            modelBuilder.Entity<FinancialStatus>()
                .HasIndex(fse => fse.Source);

            modelBuilder.Entity<FinappUser>().HasKey(fu => fu.Name);

            base.OnModelCreating(modelBuilder);
        }
    }
}