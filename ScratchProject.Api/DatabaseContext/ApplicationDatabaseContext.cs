using Microsoft.EntityFrameworkCore;
using ScratchProject.Api.Models;

namespace ScratchProject.Api.DatabaseContext
{
    public class ApplicationDatabaseContext: DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions options): base(options) 
        { 
        }

        public ApplicationDatabaseContext()
        {
            
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasData(new City { Id = Guid.Parse("D4E62AD2-30B6-4FC0-96CE-D43720101B4E"), CityName = "Cumilla" });
            modelBuilder.Entity<City>().HasData(new City { Id = Guid.Parse("5A662592-E02E-4220-8DEB-5909C95C6498"), CityName = "Dhaka" });
        }
    }
}
