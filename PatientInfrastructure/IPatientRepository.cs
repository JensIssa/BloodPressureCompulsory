using Domain;

namespace PatientInfrastructure
{
    public interface IPatientRepository
    {

       List<Patient> GetAllPatients();

       Patient CreatePatient(Patient patient);

       void UpdatePatient(int ssn);

       void DeletePatient(int ssn);

       Patient GetPatient(int ssn);

    }

}

