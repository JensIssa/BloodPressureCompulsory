using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementInfrastructure.Interfaces
{
    /// <summary>
    /// Interface for the measurement repository
    /// </summary>
    public interface IMeasurementRepository
    {
        /// <summary>
        /// Rebuilds the Measurement database
        /// </summary>
        void Rebuild();

        /// <summary>
        /// Adds a measurement to the database
        /// </summary>
        /// <param name="measurement"></param>
        /// <returns></returns>
        Task<Measurement> AddMeasurementAsync(Measurement measurement);

        /// <summary>
        /// Gets measurements by the patient ssn
        /// </summary>
        /// <param name="patientSSN">The social security number of the patient</param>
        /// <returns></returns>
        Task<ICollection<Measurement>> GetMeasurementsByPatientSSNAsync(string patientSSN);

        /// <summary>
        /// Deletes all the measurements associated with the patientSSN
        /// </summary>
        /// <param name="patientSSN">The social security number of the patient</param>
        /// <returns></returns>
        Task DeleteMeasurementsByPatientSSNAsync(string patientSSN);

        /// <summary>
        /// Deletes a measurement by id
        /// </summary>
        /// <param name="measurementId">The id of the measurement</param>
        /// <returns></returns>
        Task DeleteMeasurementByIdAsync(int measurementId);

        /// <summary>
        /// Updates the measurement
        /// </summary>
        /// <param name="measurementId">The id of the measurement</param>
        /// <param name="measurement"></param>
        /// <returns></returns>
        Task UpdateMeasurementAsync(int  measurementId, Measurement measurement);

        /// <summary>
        /// Marks the measurement as seen
        /// </summary>
        /// <param name="measurementId">The id of the measurement</param>
        /// <returns></returns>
        Task MarkMeasurementAsSeenAsync(int measurementId);

    }
}
