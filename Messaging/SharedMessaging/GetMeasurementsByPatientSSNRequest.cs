using Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.SharedMessaging
{
    public class GetMeasurementsByPatientSSNRequest
    {
        public string PatientSSN { get; set; }

        public ICollection<Measurement> Measurements { get; set; }
        public GetMeasurementsByPatientSSNRequest(string patientSSN, ICollection<Measurement> measurements)
        {
            PatientSSN = patientSSN;
            Measurements = measurements;
        }
    }
}
