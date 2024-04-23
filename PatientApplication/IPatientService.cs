using Domain;
using Domain.DTO;
using PatientApplication.DTO;

namespace PatientApplication
{
    public interface IPatientService

    {

        /// <summary>
        /// Gets all patients
        /// </summary>
        /// <returns></returns>
        Task<List<Patient>> GetAllPatients();
        /// <summary>
        /// Rebuilds the database
        /// </summary>
        void RebuildDb();

        /// <summary>
        /// Adds a patient to the database
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>

        Task<Patient> AddPatient(PatientDTO patient);

        /// <summary>
        /// Updates a patient in the database
        /// </summary>
        /// <param name="ssn">The patients social security number</param>
        /// <param name="patient">The patient being updated</param>
        /// <returns></returns>
        Task UpdatePatient(string ssn, PatientDTO patient);

        /// <summary>
        /// Deletes a patient from the database
        /// </summary>
        /// <param name="ssn">The patients social security number</param>
        /// <returns></returns>

        Task DeletePatient(string ssn);

        /// <summary>
        /// Gets a patient from the database
        /// </summary>
        /// <param name="ssn">The patients social security number</param>
        /// <returns></returns>

        Task<Patient> GetPatient(string ssn);

        Task AddMeasurementToPatient(Measurement measurement);
    }
}
