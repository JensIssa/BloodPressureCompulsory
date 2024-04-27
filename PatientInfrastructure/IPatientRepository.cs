using Domain;
using Domain.DTO;
using System.Runtime.CompilerServices;

namespace PatientInfrastructure
{
    public interface IPatientRepository
    {

        Task<List<Patient>> GetAllPatients();

       Task<Patient> CreatePatient(Patient patient);

       Task UpdatePatient(string ssn, Patient patient);

       Task DeletePatient(string ssn);

       Task<Patient> GetPatient(string ssn);


        void RebuildDb();

    }

}

