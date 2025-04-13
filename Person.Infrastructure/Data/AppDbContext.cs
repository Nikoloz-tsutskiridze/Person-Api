using BasePerson.Core.Domains;
using Microsoft.EntityFrameworkCore;
using Person.Core.Domains;

namespace Person.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> People { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PhoneRelativePerson> PhoneRelativePeople { get; set; }
        public DbSet<PeopleRelative> PeopleRelative { get; set; }

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

            //PhoneRelative
            modelBuilder.Entity<Phone>()
            .HasMany(p => p.PhoneRelativePeople)
            .WithOne(prp => prp.Phone)
            .HasForeignKey(prp => prp.PhoneId);


            modelBuilder.Entity<Customer>()
            .HasMany(p => p.PhoneRelativePeople)
            .WithOne(prp => prp.Person)
            .HasForeignKey(prp => prp.PersonId);
            //PhoneRelative

                

            modelBuilder.Entity<Customer>()
            .HasMany(p => p.FirstPersonRelatives)
            .WithOne(prp => prp.FirstPerson)
            .HasForeignKey(prp => prp.FirstPersonId); 

            modelBuilder.Entity<Customer>()
            .HasMany(p => p.SecondPersonRelatives)
            .WithOne(prp => prp.SecondPerson)
            .HasForeignKey(prp => prp.SecondPersonId);
        }
    }
}
