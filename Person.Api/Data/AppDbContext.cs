using Microsoft.EntityFrameworkCore;
using Person.Api.Domains;

namespace Person.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> People { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Cities (Example Data)
            modelBuilder.Entity<City>().HasData(new List<City>
            {
                new City { Id = 1, Name = "Tbilisi" },
                new City { Id = 2, Name = "Batumi" },
                new City { Id = 3, Name = "Kutaisi" }
            });
        }
    }
}
