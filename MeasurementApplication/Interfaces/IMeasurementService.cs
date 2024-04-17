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
        Task<Measurement> AddMeasurementAsync(MeasurementDTO measurementDto);

        /// <summary>
        /// Rebuilds the Measurement database
        /// </summary>
        void Rebuild();
    }
}
