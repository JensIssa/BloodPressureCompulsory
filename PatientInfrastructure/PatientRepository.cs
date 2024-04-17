using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Patient> DeletePatient(int ssn)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(ssn));
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                await _dbContext.SaveChangesAsync();
            }
            return patient;
        }

        public Task<List<Patient>> GetAllPatients()
        {
            return _dbContext.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatient(int ssn)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(ssn));
            return  patient;
        }

        public void RebuildDb()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        public async Task<Patient> UpdatePatient(int ssn)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(ssn));
            if (patient != null)
            {
                _dbContext.Patients.Update(patient);
                await _dbContext.SaveChangesAsync();
            }
            return patient;
        }
    }
}
