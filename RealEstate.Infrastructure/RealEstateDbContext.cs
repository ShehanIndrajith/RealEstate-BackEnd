using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Entities;

namespace RealEstate.Infrastructure
{
    public class RealEstateDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Properties> Properties { get; set; }

        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: define primary keys explicitly
            modelBuilder.Entity<Users>().HasKey(u => u.UserID);
            modelBuilder.Entity<Properties>().HasKey(p => p.PropertyID);

            // Foreign key relationship: Property → Agent (User)
            modelBuilder.Entity<Properties>()
                .HasOne(p => p.Users)
                .WithMany(u => u.Properties) // Make sure Users class has ICollection<Properties> Properties
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.SetNull); // Prevent cascade delete

            // Check constraint for PropertyType
            modelBuilder.Entity<Properties>()
                .HasCheckConstraint("CK_PropertyType", "[PropertyType] IN ('Commercial', 'Land', 'Apartment', 'House')");

            // Check constraint for Status
            modelBuilder.Entity<Properties>()
                .HasCheckConstraint("CK_PropertyStatus", "[Status] IN ('For Sale', 'For Rent')");
        }
        
        
    }
}
