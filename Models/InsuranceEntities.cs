// Models/InsuranceEntities.cs
using Microsoft.EntityFrameworkCore;

// Namespace for the MVC application
namespace CarInsurance.Models
{
    // DbContext for the Insurance database
    public class InsuranceEntities : DbContext
    {
        // Constructor to pass options
        public InsuranceEntities(DbContextOptions<InsuranceEntities> options)
            : base(options)
        {
        }

        // DbSet for Insuree entities
        public DbSet<Insuree> Insurees { get; set; }

        // Configure model properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Insuree>().ToTable("Insurees");
            modelBuilder.Entity<Insuree>().Property(i => i.Quote).HasColumnType("decimal(18,2)");
        }
    }
}