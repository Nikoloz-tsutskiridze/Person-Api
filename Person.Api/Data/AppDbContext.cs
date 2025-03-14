using Microsoft.EntityFrameworkCore;
using Person.Api.Domains;

namespace Person.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Domains.Person> Persons { get; set; }
        public DbSet<Phone> Phones { get; set; }

    }
}
