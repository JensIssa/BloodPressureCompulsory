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
        public RepositoryDBContext(DbContextOptions<RepositoryDBContext> options, ServiceLifetime service) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Patient-db;Database=Measurement;User Id=sa;Password=SuperSecret7!;Trusted_Connection=False;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Patient>().HasKey(p => p.SSN);
        }

        public DbSet<Patient> Patients { get; set;}

    }
}
