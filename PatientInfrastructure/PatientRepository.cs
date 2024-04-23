using Domain;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace PatientInfrastructure
{
    public class PatientRepository : IPatientRepository
    {
        private RepositoryDBContext _dbContext;

        public PatientRepository(RepositoryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
            return patient;

        }

        public async Task DeletePatient(string ssn)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(ssn));
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task<List<Patient>> GetAllPatients()
        {
            return _dbContext.Patients.Include(p => p.Measurements).ToListAsync();
        }

        public async Task<Patient> GetPatient(string ssn)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(ssn));
            return  patient;
        }

        public void RebuildDb()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        public async Task UpdatePatient(string ssn, Patient patient)
        {
            var patientUpdate = _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(ssn));
            if (patient != null)
            {
                patientUpdate.Email = patient.Email;
                patientUpdate.Name = patient.Name;
                patientUpdate.SSN = patient.SSN;
                _dbContext.Patients.Update(patientUpdate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddMeasurementToPatient(Measurement measurement)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(measurement.PatientSSN));

            if (patient != null)
            {
                patient.Measurements.Add(measurement);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
