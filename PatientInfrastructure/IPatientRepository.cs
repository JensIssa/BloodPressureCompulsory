using Domain;
using Domain.DTO;
using System.Runtime.CompilerServices;

namespace PatientInfrastructure
{
    public interface IPatientRepository
    {

        Task<List<PatientBE>> GetAllPatients();

       Task<PatientBE> CreatePatient(PatientBE patient);

       Task UpdatePatient(string ssn, PatientBE patient);

       Task DeletePatient(string ssn);

       Task<PatientBE> GetPatient(string ssn);


        void RebuildDb();

    }

}

