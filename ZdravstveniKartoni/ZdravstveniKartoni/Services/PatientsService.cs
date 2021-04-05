using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Helpers;

namespace ZdravstveniKartoni.Services
{
    public interface IPatientsService
    {
        Patients GetPatientDetailsByPatientId(int userId);
        List<DiseaseRecords> GetDiseaseRecordsByPatientId(int userId);
        List<Receipt> GetReceiptsByPatientId(int userId);
        List<Appointments> GetAppointmentsByPatientId(int userId);
        Appointments CreateNewAppointment(Appointments model);
    }

    public class PatientsService : IPatientsService
    {
        private DataContext _dataContext;

        public PatientsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Appointments CreateNewAppointment(Appointments model)
        {
            _dataContext.Appointments.Add(model);
            _dataContext.SaveChanges();

            return _dataContext.Appointments.Where(a => a.UserId == model.UserId)
                .Select(ap => new Appointments()
                {
                    UserId = ap.UserId,
                    DateOfAppointment = ap.DateOfAppointment,
                    DoctorsName = ap.DoctorsName,
                    Id = ap.Id,
                    ReasonForVisit = ap.ReasonForVisit,
                    Status = ap.Status
                }).FirstOrDefault();
        }

        public List<Appointments> GetAppointmentsByPatientId(int userId)
        {
            var appointments = _dataContext.Appointments.Where(a => a.UserId == userId).ToList();

            if(appointments == null)
            {
                return new List<Appointments>();
            }

            return appointments;
        }

        public List<DiseaseRecords> GetDiseaseRecordsByPatientId(int userId)
        {
            var diseaseRecords = _dataContext.DiseaseRecords.Where(dr => dr.UserId == userId).ToList();
            
            if(diseaseRecords == null)
            {
                return new List<DiseaseRecords>();
            }

            return diseaseRecords;
        }

        public Patients GetPatientDetailsByPatientId(int userId)
        {
            return _dataContext.Patients.Where(p => p.UserId == userId)
                .Select(pat => new Patients()
                {
                    Id = pat.Id,
                    UserId = pat.UserId,
                    Address = pat.Address,
                    BloodType = pat.BloodType,
                    DateOfBirth = pat.DateOfBirth,
                    FirstName = pat.FirstName,
                    LastName = pat.LastName,
                    Gender = pat.Gender,
                    JMBG = pat.JMBG,
                    RHFaktor = pat.RHFaktor,
                    RoleId = pat.RoleId
                }).FirstOrDefault();
        }

        public List<Receipt> GetReceiptsByPatientId(int userId)
        {
            var receipts = _dataContext.Receipts.Where(r => r.UserId == userId).ToList();

            if(receipts == null)
            {
                return new List<Receipt>();
            }

            return receipts;
        }
    }
}
