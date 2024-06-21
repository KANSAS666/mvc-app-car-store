using CarStore.Models;
using Microsoft.EntityFrameworkCore;
namespace CarStore.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarEntity>().HasKey(c => c.Id);
            modelBuilder.Entity<CarEntity>().Property(c => c.Mark).IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<CarEntity>().Property(c => c.Model).IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<CarEntity>().Property(c => c.Color).IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<CarEntity>().Property(c => c.ImagePath).IsRequired(false);
        }

        public DbSet<CarEntity> Cars { get; set; }
    }
}
