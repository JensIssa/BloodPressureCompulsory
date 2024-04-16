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

        public Patient CreatePatient(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            _dbContext.SaveChanges();
            return patient;

        }

        public void DeletePatient(int ssn)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(ssn));
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                _dbContext.SaveChanges();
            }
        }

        public List<Patient> GetAllPatients()
        {
            return _dbContext.Patients.ToList();
        }

        public Patient GetPatient(int ssn)
        {
            throw new NotImplementedException();
        }

        public void UpdatePatient(int ssn)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.SSN.Equals(ssn));
            if (patient != null)
            {
                _dbContext.Patients.Update(patient);
                _dbContext.SaveChanges();
            }
        }
    }
}
