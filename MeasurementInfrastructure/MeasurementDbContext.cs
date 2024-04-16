using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementInfrastructure
{
    public class MeasurementDbContext : DbContext
    {
        public DbSet<Measurement> Measurements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Measurement-db,1433;Database=Measurement;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measurement>().HasKey(e => e.Id);

            modelBuilder.Entity<Measurement>().Property(m => m.Id).ValueGeneratedOnAdd();
        }

    }
}
