using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravstveniKartoni.Entities
{
    public class PatientData
    {
        public int Id { get; set; }
        public int DoctorsPatientsId { get; set; }
        public string IllnessName { get; set; }
        public string IllnessStatus { get; set; }
        public string IllnessTherapy { get; set; }
        public string Medication { get; set; }
        public bool AlergicToPennicilin { get; set; }
        public bool AlergicToPolen { get; set; }
        public bool HasAsthma { get; set; }
        public bool HasDiabetes { get; set; }
    }
}
