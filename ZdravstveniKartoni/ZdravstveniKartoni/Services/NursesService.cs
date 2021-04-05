using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravstveniKartoni.Dtos;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Helpers;

namespace ZdravstveniKartoni.Services
{
    public interface INursesService
    {
        List<AppointmentsDto> GetAppointments();
        bool ApproveAppointment(int userId);
        bool DeclineAppointment(int userId);
        List<AppointmentsDto> GetApprovedAppointments();
        List<AppointmentsDto> GetDeclinedAppointments();
        List<AppointmentsDto> SearchAppointments(string searchValue);
        List<AppointmentsDto> SearchApprovedAppointments(string searchValue);
    }
    public class NursesService : INursesService
    {
        private DataContext _dataContext;

        public NursesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool ApproveAppointment(int userId)
        {
            var appointment = _dataContext.Appointments.Where(a => a.UserId == userId && a.Status == 1).FirstOrDefault();

            if(appointment == null)
            {
                return false;
            }

            appointment.Status = 2;

            _dataContext.Appointments.Update(appointment);
            _dataContext.SaveChanges();

            return true;
        }

        public bool DeclineAppointment(int userId)
        {
            var appointment = _dataContext.Appointments.Where(a => a.UserId == userId && a.Status == 1).FirstOrDefault();

            if (appointment == null)
            {
                return false;
            }

            appointment.Status = 3;

            _dataContext.Appointments.Update(appointment);
            _dataContext.SaveChanges();

            return true;
        }

        public List<AppointmentsDto> GetAppointments()
        {
            var appointments = _dataContext.Appointments.Where(a => a.Status == 1).ToList();
            List<AppointmentsDto> appList = new List<AppointmentsDto>();
            
            if (appointments == null)
            {
                return new List<AppointmentsDto>();
            }
            foreach(var app in appointments)
            {
                var user = _dataContext.Users.Find(app.UserId);
                appList.Add(new AppointmentsDto()
                {
                    Id = app.Id,
                    DateOfAppointment = app.DateOfAppointment,
                    DoctorsName = app.DoctorsName,
                    ReasonForVisit = app.ReasonForVisit,
                    Status = app.Status,
                    UserId = app.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }

            return appList;
        }

        public List<AppointmentsDto> GetApprovedAppointments()
        {
            var appointments = _dataContext.Appointments.Where(a => a.Status == 2).ToList();
            List<AppointmentsDto> appList = new List<AppointmentsDto>();

            if (appointments == null)
            {
                return new List<AppointmentsDto>();
            }

            foreach (var app in appointments)
            {
                var user = _dataContext.Users.Find(app.UserId);
                appList.Add(new AppointmentsDto()
                {
                    Id = app.Id,
                    DateOfAppointment = app.DateOfAppointment,
                    DoctorsName = app.DoctorsName,
                    ReasonForVisit = app.ReasonForVisit,
                    Status = app.Status,
                    UserId = app.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }

            return appList;
        }

        public List<AppointmentsDto> GetDeclinedAppointments()
        {
            var appointments = _dataContext.Appointments.Where(a => a.Status == 3).ToList();
            List<AppointmentsDto> appList = new List<AppointmentsDto>();

            if (appointments == null)
            {
                return new List<AppointmentsDto>();
            }

            foreach (var app in appointments)
            {
                var user = _dataContext.Users.Find(app.UserId);
                appList.Add(new AppointmentsDto()
                {
                    Id = app.Id,
                    DateOfAppointment = app.DateOfAppointment,
                    DoctorsName = app.DoctorsName,
                    ReasonForVisit = app.ReasonForVisit,
                    Status = app.Status,
                    UserId = app.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }

            return appList;
        }

        public List<AppointmentsDto> SearchAppointments(string searchValue)
        {
            try
            {
                var appointments = _dataContext.Appointments.Where(a => a.DateOfAppointment == searchValue && a.Status == 1).ToList();
                List<AppointmentsDto> app = new List<AppointmentsDto>();

                if (appointments == null)
                {
                    return new List<AppointmentsDto>();
                }
                foreach (var a in appointments)
                {
                    app.Add(new AppointmentsDto()
                    {
                        Id = a.Id,
                        DateOfAppointment = a.DateOfAppointment,
                        DoctorsName = a.DoctorsName,
                        FirstName = _dataContext.Users.Where(u => u.Id == a.UserId).FirstOrDefault().FirstName,
                        LastName = _dataContext.Users.Where(u => u.Id == a.UserId).FirstOrDefault().LastName,
                        UserId = a.UserId,
                        ReasonForVisit = a.ReasonForVisit,
                        Status = a.Status
                    });
                }

                return app;
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                return new List<AppointmentsDto>();
            }
        }

        public List<AppointmentsDto> SearchApprovedAppointments(string searchValue)
        {
            try
            {
                var appointments = _dataContext.Appointments.Where(a => a.DateOfAppointment == searchValue && a.Status == 2).ToList();
                List<AppointmentsDto> app = new List<AppointmentsDto>();

                if (appointments == null)
                {
                    return new List<AppointmentsDto>();
                }
                foreach (var a in appointments)
                {
                    app.Add(new AppointmentsDto()
                    {
                        Id = a.Id,
                        DateOfAppointment = a.DateOfAppointment,
                        DoctorsName = a.DoctorsName,
                        FirstName = _dataContext.Users.Where(u => u.Id == a.UserId).FirstOrDefault().FirstName,
                        LastName = _dataContext.Users.Where(u => u.Id == a.UserId).FirstOrDefault().LastName,
                        UserId = a.UserId,
                        ReasonForVisit = a.ReasonForVisit,
                        Status = a.Status
                    });
                }

                return app;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return new List<AppointmentsDto>();
            }
        }
    }
}
