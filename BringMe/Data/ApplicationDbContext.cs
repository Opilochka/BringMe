using BringMe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BringMe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Way> Ways { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<DeliveryWay> DeliveryWays { get;set; }

        public DbSet<DeliveryTest> DeliveryTest { get; set; }
        public DbSet<DeliveryWayTest> DeliveryWaysTest { get; set; }

    }
}