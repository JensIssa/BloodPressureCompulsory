using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientInfrastructure
{
    public class RepositoryDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Patient-db;Database=Patient;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PatientBE>().HasKey(p => p.SSN);

            modelBuilder.Entity<PatientBE>().HasData(
                new PatientBE()
                    { SSN = "123", Name = "Olaf", Email = "Olaf@mail.com" },
                new PatientBE()
                    { SSN = "1234", Name = "Jens", Email = "Jens@mail.com" },
                new PatientBE()
                    { SSN = "12345", Name = "Benny", Email = "Benny@mail.com" },
                new PatientBE()
                    { SSN = "123456", Name = "Lars", Email = "Lars@mail.com" },
                new PatientBE()
                    { SSN = "1234567", Name = "Vladimir", Email = "Vladimir@mail.com",  });
        }

        public DbSet<PatientBE> Patients { get; set;}

    }
}
