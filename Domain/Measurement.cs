using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Measurement
    {
        /// <summary>
        /// Id of measurement
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date the measurement is taken
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The systolic value of the measurement
        /// </summary>
        public int Systolic { get; set; }

        /// <summary>
        /// The diastolic value of the measurement
        /// </summary>
        public int Diastolic { get; set; }

        /// <summary>
        /// Patient's social security number
        /// </summary>
        public string PatientSSN { get; set; }
    }
}
