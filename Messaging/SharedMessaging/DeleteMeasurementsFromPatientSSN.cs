using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.SharedMessaging
{
    public class DeleteMeasurementsFromPatientSSN
    {
        public string Message { get; set; }
        public string PatientSSN { get; set; }

        public DeleteMeasurementsFromPatientSSN(string message, string patientSSN)
        {
            Message = message;
            PatientSSN = patientSSN;
        }
    }
}
