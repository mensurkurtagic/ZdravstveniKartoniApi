using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravstveniKartoni.Entities
{
    public class Receipt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MedicineDescription { get; set; }
        public string Therapy { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public int Status { get; set; }
    }
}
