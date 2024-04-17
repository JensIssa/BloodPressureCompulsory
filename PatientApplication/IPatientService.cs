using Domain;
using PatientApplication.DTO;

namespace PatientApplication
{
    public interface IPatientService

    {

        Task<List<Patient>> GetAllPatients();
        void RebuildDb();

        Task<Patient> AddPatient(PatientDTO patient);

        Task<Patient> UpdatePatient(int ssn);

        Task<Patient> DeletePatient(int ssn);

        Task<Patient> GetPatient(int ssn);

    }
}
