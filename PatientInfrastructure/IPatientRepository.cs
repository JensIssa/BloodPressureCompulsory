using Domain;
using System.Runtime.CompilerServices;

namespace PatientInfrastructure
{
    public interface IPatientRepository
    {

        Task<List<Patient>> GetAllPatients();

       Task<Patient> CreatePatient(Patient patient);

       Task<Patient> UpdatePatient(int ssn);

       Task<Patient> DeletePatient(int ssn);

       Task<Patient> GetPatient(int ssn);

       void RebuildDb();

    }

}

