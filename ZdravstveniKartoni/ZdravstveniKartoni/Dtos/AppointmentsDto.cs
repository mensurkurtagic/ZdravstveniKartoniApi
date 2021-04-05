using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravstveniKartoni.Dtos
{
    public class AppointmentsDto
    {
        public int Id { get; set; }
        public string DoctorsName { get; set; }
        public string DateOfAppointment { get; set; }
        public string ReasonForVisit { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
