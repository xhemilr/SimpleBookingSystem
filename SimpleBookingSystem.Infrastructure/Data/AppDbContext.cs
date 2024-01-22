using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Core.Entities;

namespace SimpleBookingSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {           
        }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Resource>().HasData(
                new Resource { Id = 1, Name = "Resource1", Quantity = 10 },
                new Resource { Id = 2, Name = "Resource2", Quantity = 10 },
                new Resource { Id = 3, Name = "Resource3", Quantity = 10 },
                new Resource { Id = 4, Name = "Resource4", Quantity = 10 },
                new Resource { Id = 5, Name = "Resource5", Quantity = 10 }
            );
        }
    }
}
