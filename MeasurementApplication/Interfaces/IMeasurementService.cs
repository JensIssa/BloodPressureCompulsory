using Domain;
using MeasurementApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementApplication.Interfaces
{
    /// <summary>
    /// Interface for the measurement service
    /// </summary>
    public interface IMeasurementService
    {
        /// <summary>
        /// Adds a measurement to the database
        /// </summary>
        /// <param name="measurementDto"></param>
        /// <returns></returns>
        Task<Measurement> AddMeasurementAsync(CreateMeasurementDTO measurementDto);

        /// <summary>
        /// Gets the measurements by the patient ssn
        /// </summary>
        /// <param name="patientSSN">The patient's social security number</param>
        /// <returns></returns>
        Task<IEnumerable<Measurement>> GetMeasurementsByPatientSSNAsync(string patientSSN);

    
        /// <summary>
        /// Deletes all the measurements associated with the patient's SSN
        /// </summary>
        /// <param name="patientSSN">The social security number of the patient</param>
        /// <returns></returns>
        Task DeleteMeasurementsByPatientSSNAsync(string patientSSN);

        /// <summary>
        /// Deletes a measurement by its id
        /// </summary>
        /// <param name="measurementId">The id of the measurement</param>
        /// <returns></returns>
        Task DeleteMeasurementById(int measurementId);

        /// <summary>
        /// Updates the measurement
        /// </summary>
        /// <param name="measurementId">The id of the measurement</param>
        /// <param name="measurement"></param>
        /// <returns></returns>
        Task UpdateMeasurementAsync(int measurementId, UpdateMeasurementDTO measurement);

        /// <summary>
        /// Marks the measurement as seen by the doctor
        /// </summary>
        /// <param name="measurementId">The id of the measurement</param>
        /// <returns></returns>
        Task MarkMeasurementAsSeenAsync(int measurementId);

        /// <summary>
        /// Rebuilds the Measurement database
        /// </summary>
        void Rebuild();
    }
}
