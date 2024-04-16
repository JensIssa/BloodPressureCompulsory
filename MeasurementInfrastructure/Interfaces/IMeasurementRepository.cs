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
    }
}
