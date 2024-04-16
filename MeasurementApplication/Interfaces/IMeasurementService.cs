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
        /// Rebuilds the Measurement database
        /// </summary>
        void Rebuild();
    }
}
