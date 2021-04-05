using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravstveniKartoni.Entities
{
    public class DiseaseRecords
    {
        public int Id { get; set; }
        public string DateOfVisit { get; set; }
        public string Description { get; set; }
        public string Diagnose { get; set; }
        public string Therapy { get; set; }
        public string OrderToDoctor { get; set; }
        public string CanWork { get; set; }
        public int UserId { get; set; }
    }
}
