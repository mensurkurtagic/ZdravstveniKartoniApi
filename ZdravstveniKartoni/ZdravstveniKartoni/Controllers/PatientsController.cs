using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Services;

namespace ZdravstveniKartoni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private IPatientsService _patientsService;

        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        /// <summary>
        /// get patients details by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Pacijent")]
        [HttpGet("getPatientDetailsByPatientId/{userId}")]
        public Patients GetPatientDetailsByPatientId(int userId)
        {
            return _patientsService.GetPatientDetailsByPatientId(userId);
        }

        /// <summary>
        /// get disease records by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("getDiseaseRecordsByPatientId/{userId}")]
        public List<DiseaseRecords> GetDiseaseRecordsByPatientId(int userId)
        {
            return _patientsService.GetDiseaseRecordsByPatientId(userId);
        }

        /// <summary>
        /// get receipts by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("getReceiptsByPatientId/{userId}")]
        public List<Receipt> GetReceiptsByPatientId(int userId)
        {
            return _patientsService.GetReceiptsByPatientId(userId);
        }

        /// <summary>
        /// get appointments for patient by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("getPatientsAppointmentsByPatientId/{userId}")]
        public List<Appointments> GetPatientsAppointmentsByPatientId(int userId)
        {
            return _patientsService.GetAppointmentsByPatientId(userId);
        }

        /// <summary>
        /// create new appointment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("createNewAppointment")]
        public Appointments CreateNewAppointment([FromBody] Appointments model)
        {
            return _patientsService.CreateNewAppointment(model);
        }
    }
}