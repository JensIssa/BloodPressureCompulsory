﻿using System.Diagnostics.Metrics;

namespace Domain
{
    public class Patient
    {

        /// <summary>
        /// The Patient's social security number.
        /// </summary>

        public string SSN { get; set; }

        /// <summary>
        /// The name of the patient
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The email of the patient
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();

    }
}
