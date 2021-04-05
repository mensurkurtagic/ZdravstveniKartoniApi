using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravstveniKartoni.Dtos;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Helpers;

namespace ZdravstveniKartoni.Services
{
    public interface IDoctorPatientManagementService
    {
        List<Patients> GetDoctorsPatientsByDoctorId(int DoctorId);
        Patients GetPatientById(int patientId);
        DoctorDto GetDoctorById(int doctorId);
        Patients PatchUpdatePatientDetailsByPatientId(int patientId, Patients model);
        List<DiseaseRecords> GetDiseaseRecordsByPatientId(int userId);
        DiseaseRecords PostCreateNewDiseaseRecordByPatientId(DiseaseRecords model);
        Receipt PostCreateNewReceiptByPatientId(Receipt model);
        List<Receipt> GetReceiptsByPatientId(int userId);
        List<Patients> SearchPatients(string searchValue);
        DiseaseRecords GetDiseaseRecordByDiseaseRecordId(int id);
        DiseaseRecords UpdateDiseaseRecordByDiseaseRecordId(int id, DiseaseRecords model);
    }

    public class DoctorPatientManagementService : IDoctorPatientManagementService
    {
        private DataContext _context;

        public DoctorPatientManagementService(DataContext context)
        {
            _context = context;
        }

        public DiseaseRecords GetDiseaseRecordByDiseaseRecordId(int id)
        {
            try
            {
                return _context.DiseaseRecords.Where(dr => dr.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }

        public List<DiseaseRecords> GetDiseaseRecordsByPatientId(int userId)
        {
            var diseaseRecords = _context.DiseaseRecords.Where(dr => dr.UserId == userId).ToList();

            if(diseaseRecords == null)
            {
                return new List<DiseaseRecords>();
            }

            return diseaseRecords;
        }

        public DoctorDto GetDoctorById(int doctorId)
        {
            try
            {
                var doctor = _context.Users.Where(u => u.Id == doctorId).FirstOrDefault();
                //imeIPrezime = doctor.FirstName + " " + doctor.LastName;

                return new DoctorDto()
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName
                };
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }

        public List<Patients> GetDoctorsPatientsByDoctorId(int DoctorId)
        {
            try
            {
                if (DoctorId == null)
                {
                    return null;
                }
                var doctorPatients = _context.DoctorsPatients.Where(dp => dp.DoctorId == DoctorId).ToList();
                List<Patients> patients = new List<Patients>();
                foreach (var dp in doctorPatients)
                {
                    patients.Add(_context.Patients.Where(p => p.Id == dp.PatientId).FirstOrDefault());
                }

                return patients;
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }

        public Patients GetPatientById(int patientId)
        {
            return _context.Patients.Where(p => p.UserId == patientId).Select(pat => new Patients
            {
                Id = pat.Id,
                JMBG = pat.JMBG,
                Gender = pat.Gender,
                BloodType = pat.BloodType,
                DateOfBirth = pat.DateOfBirth,
                FirstName = pat.FirstName,
                LastName = pat.LastName,
                RHFaktor = pat.RHFaktor,
                RoleId = pat.RoleId,
                UserId = pat.UserId,
                Address = pat.Address
            }).FirstOrDefault();
        }

        public List<Receipt> GetReceiptsByPatientId(int userId)
        {
            var receipts = _context.Receipts.Where(r => r.UserId == userId).ToList();

            if(receipts == null)
            {
                return new List<Receipt>();
            }

            return receipts;
        }

        public Patients PatchUpdatePatientDetailsByPatientId(int patientId, Patients model)
        {
            var patient = _context.Patients.Where(p => p.UserId == patientId).Select(pat => new Patients
            {
                Id = pat.Id,
                JMBG = pat.JMBG,
                Gender = pat.Gender,
                BloodType = pat.BloodType,
                DateOfBirth = pat.DateOfBirth,
                FirstName = pat.FirstName,
                LastName = pat.LastName,
                RHFaktor = pat.RHFaktor,
                RoleId = pat.RoleId,
                UserId = pat.UserId,
                Address = pat.Address
            }).FirstOrDefault();
            
            model.UserId = patient.UserId;
            model.RoleId = patient.RoleId;
            model.Id = patient.Id;

            _context.Update(model);
            _context.SaveChanges();

            return _context.Patients.Where(p => p.UserId == patientId).Select(pat => new Patients
            {
                Id = pat.Id,
                JMBG = pat.JMBG,
                Gender = pat.Gender,
                BloodType = pat.BloodType,
                DateOfBirth = pat.DateOfBirth,
                FirstName = pat.FirstName,
                LastName = pat.LastName,
                RHFaktor = pat.RHFaktor,
                RoleId = pat.RoleId,
                UserId = pat.UserId,
                Address = pat.Address
            }).FirstOrDefault();
        }

        public DiseaseRecords PostCreateNewDiseaseRecordByPatientId(DiseaseRecords model)
        {
            _context.DiseaseRecords.Add(model);
            _context.SaveChanges();

            return _context.DiseaseRecords.Where(dr => dr.UserId == model.UserId)
                .Select(ndr => new DiseaseRecords()
                {
                    Id = ndr.Id,
                    UserId = ndr.UserId,
                    CanWork = ndr.CanWork,
                    DateOfVisit = ndr.DateOfVisit,
                    Description = ndr.Description,
                    Diagnose = ndr.Diagnose,
                    OrderToDoctor = ndr.OrderToDoctor,
                    Therapy = ndr.Therapy
                }).FirstOrDefault();
        }

        public Receipt PostCreateNewReceiptByPatientId(Receipt model)
        {
            _context.Receipts.Add(model);
            _context.SaveChanges();

            return _context.Receipts.Where(r => r.UserId == model.UserId)
                .Select(rc => new Receipt()
                {
                    Id = rc.Id,
                    UserId = rc.UserId,
                    Description = rc.Description,
                    DoctorId = rc.DoctorId,
                    MedicineDescription = rc.MedicineDescription,
                    Therapy = rc.Therapy,
                    Title = rc.Title,
                    Status = 1
                }).FirstOrDefault();
        }

        public List<Patients> SearchPatients(string searchValue)
        {
            return _context.Patients.Where(p => p.FirstName.Contains(searchValue) || p.LastName.Contains(searchValue)).ToList();
        }

        public DiseaseRecords UpdateDiseaseRecordByDiseaseRecordId(int id, DiseaseRecords model)
        {
            try
            {
                var diseaseRecord = _context.DiseaseRecords.Where(dr => dr.Id == id).Select(d => new DiseaseRecords()
                {
                    Id = d.Id,
                    CanWork = d.CanWork,
                    DateOfVisit = d.DateOfVisit,
                    Description = d.Description,
                    Diagnose = d.Diagnose,
                    OrderToDoctor = d.OrderToDoctor,
                    Therapy = d.Therapy,
                    UserId = d.UserId
                }).FirstOrDefault();

                model.Id = diseaseRecord.Id;
                model.UserId = diseaseRecord.UserId;

                _context.Update(model);
                _context.SaveChanges();

                return _context.DiseaseRecords.Where(dr => dr.Id == id)
                    .Select(d => new DiseaseRecords()
                    {
                        Id = d.Id,
                        CanWork = d.CanWork,
                        DateOfVisit = d.DateOfVisit,
                        Description = d.Description,
                        Diagnose = d.Diagnose,
                        OrderToDoctor = d.OrderToDoctor,
                        Therapy = d.Therapy,
                        UserId = d.UserId
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }
    }
}
