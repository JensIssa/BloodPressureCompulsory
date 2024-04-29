using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementApplication.DTO
{
    public class UpdateMeasurementDTO
    {
        public int ID { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
    }
}
