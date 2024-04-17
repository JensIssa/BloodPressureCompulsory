using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementApplication.DTO
{
    public class CreateMeasurementDTO
    {
        public int Systolic { get; set; }
        public int Diastolic { get; set; }

        public string PatientSSN { get; set; }

    }
}
