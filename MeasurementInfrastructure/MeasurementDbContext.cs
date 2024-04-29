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
            optionsBuilder.UseSqlServer("Server=Measurement-db;Database=Measurement;User Id=sa;Password=SuperSecret7!;TrustServerCertificate=True;", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measurement>().HasKey(e => e.Id);
            modelBuilder.Entity<Measurement>().Property(m => m.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Measurement>().Property(m => m.IsSeen).HasDefaultValue(false);

            modelBuilder.Entity<Measurement>().HasData(
                new Measurement()
                    { Id = 1, Date = DateTime.Now, Systolic = 60, Diastolic = 60, IsSeen = false, PatientSSN = "123" },
                new Measurement()
                    { Id = 2, Date = DateTime.Now, Systolic = 20, Diastolic = 30, IsSeen = false, PatientSSN = "1234" },
                new Measurement()
                    { Id = 3, Date = DateTime.Now, Systolic = 30, Diastolic = 40, IsSeen = false, PatientSSN = "12345" },
                new Measurement()
                    { Id = 4, Date = DateTime.Now, Systolic = 70, Diastolic = 55, IsSeen = false, PatientSSN = "123456" },
                new Measurement()
                    { Id = 5, Date = DateTime.Now, Systolic = 80, Diastolic = 60, IsSeen = false, PatientSSN = "1234567" });
        }

    }
}
