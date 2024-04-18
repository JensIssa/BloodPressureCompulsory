using Domain;
using PatientApplication.DTO;

namespace PatientApplication
{
    public interface IPatientService

    {

        Task<List<Patient>> GetAllPatients();
        void RebuildDb();

        Task<Patient> AddPatient(PatientDTO patient);

        Task UpdatePatient(string ssn);

        Task DeletePatient(string ssn);

        Task<Patient> GetPatient(string ssn);

    }
}
