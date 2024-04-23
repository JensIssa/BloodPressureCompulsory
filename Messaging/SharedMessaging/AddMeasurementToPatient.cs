using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.SharedMessaging
{
    public class AddMeasurementToPatient
    {
        public string Message { get; set; }
        public int MeasurementID { get; set; }
        public string PatientSSN { get; set; }
        public AddMeasurementToPatient(string message, int measurementID, string patientSSN)
        {
            Message = message;
            MeasurementID = measurementID;
            PatientSSN = patientSSN;
        }
    }
}
