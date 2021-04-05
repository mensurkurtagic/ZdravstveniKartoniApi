using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravstveniKartoni.Entities;

namespace ZdravstveniKartoni.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<DoctorsPatients> DoctorsPatients { get; set; }
        public DbSet<PatientData> PatientData { get; set; }
        public DbSet<DiseaseRecords> DiseaseRecords { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
    }
}
