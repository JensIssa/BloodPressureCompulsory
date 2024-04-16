using System.Diagnostics.Metrics;

namespace Domain
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Émail { get; set; }

        public string SSN { get; set; }

        public ICollection<Measurement> measurements { get; set; }

    }
}
