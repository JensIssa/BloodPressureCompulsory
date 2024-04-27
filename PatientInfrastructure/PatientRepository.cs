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

        public async Task<PatientBE> CreatePatient(PatientBE patient)
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

        public Task<List<PatientBE>> GetAllPatients()
        {
            return _dbContext.Patients.ToListAsync();
        }

        public async Task<PatientBE> GetPatient(string ssn)
        {
            var patient =  _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(ssn));
            return  patient;
        }

        public void RebuildDb()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        public async Task UpdatePatient(string ssn, PatientBE patient)
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
        }
    }

