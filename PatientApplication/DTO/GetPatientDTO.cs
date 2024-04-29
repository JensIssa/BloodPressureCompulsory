using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientApplication.DTO
{
    public class GetPatientDTO
    {
        public string SSN { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Measurement> Measurements { get; set; }
    }
}
