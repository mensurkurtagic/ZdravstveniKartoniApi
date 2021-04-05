using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravstveniKartoni.Entities
{
    public class Patients
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string BloodType { get; set; }
        public string JMBG { get; set; }
        public string RHFaktor { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
    }
}
