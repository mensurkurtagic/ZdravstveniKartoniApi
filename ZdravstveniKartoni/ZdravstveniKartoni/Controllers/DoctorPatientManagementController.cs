using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZdravstveniKartoni.Dtos;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Services;

namespace ZdravstveniKartoni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorPatientManagementController : ControllerBase
    {
        private IDoctorPatientManagementService _doctorPatientManagementService;

        public DoctorPatientManagementController(IDoctorPatientManagementService doctorPatientManagementService)
        {
            _doctorPatientManagementService = doctorPatientManagementService;
        }
        /// <summary>
        /// Get Doctors Patients as list by DoctorId
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        [HttpGet("getPatientsByDoctorId/{doctorId}")]
        public List<Patients> GetPatientsByDoctorId(int doctorId)
        {
            return _doctorPatientManagementService.GetDoctorsPatientsByDoctorId(doctorId);
        }

        /// <summary>
        /// Get Patient details by patient id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet("getPatientByPatientId/{patientId}")]
        public Patients GetPatientByPatientId(int patientId)
        {
            return _doctorPatientManagementService.GetPatientById(patientId);
        }

        [HttpGet("getDoctorById/{id}")]
        public DoctorDto GetDoctorById(int id)
        {
            return _doctorPatientManagementService.GetDoctorById(id);
        }

        /// <summary>
        /// Patch patient details by patient Id (or userId)
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("patchUpdatePatientDetailsByPatientId/{patientId}")]
        public Patients PatchUpdatePatientDetailsByPatientId(int patientId, Patients model)
        {
            return _doctorPatientManagementService.PatchUpdatePatientDetailsByPatientId(patientId, model);
        }

        /// <summary>
        /// Get Disease records by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("getDiseaseRecordsByPatientId/{userId}")]
        public List<DiseaseRecords> GetDiseaseRecordsByPatientId(int userId)
        {
            return _doctorPatientManagementService.GetDiseaseRecordsByPatientId(userId);
        }

        /// <summary>
        /// create new disease record for user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("postCreateNewDiseaseRecord")]
        public DiseaseRecords CreateNewDiseaseRecord([FromBody] DiseaseRecords model)
        {
            return _doctorPatientManagementService.PostCreateNewDiseaseRecordByPatientId(model);
        }

        [HttpGet("getReceiptsForPatientByPatientId/{userId}")]
        public List<Receipt> GetReceiptsForPatientByPatientId(int userId)
        {
            return _doctorPatientManagementService.GetReceiptsByPatientId(userId);
        }

        /// <summary>
        /// create new receipt for patient
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("postCreateNewReceipt")]
        public Receipt CreateNewReceipt([FromBody] Receipt model)
        {
            return _doctorPatientManagementService.PostCreateNewReceiptByPatientId(model);
        }

        /// <summary>
        /// search patients by name
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpPost("searchPatients")]
        public List<Patients> SearchPatients([FromQuery] string searchValue)
        {
            return _doctorPatientManagementService.SearchPatients(searchValue);
        }

        /// <summary>
        /// Get Disease Record By DiseaserecordId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getDiseaseRecordByDiseaseRecordId/{id}")]
        public DiseaseRecords GetDiseaseRecordByDiseaseRecordId(int id)
        {
            return _doctorPatientManagementService.GetDiseaseRecordByDiseaseRecordId(id);
        }

        /// <summary>
        /// update disease record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("updateDiseaseRecordByDiseaseRecordId/{id}")]
        public DiseaseRecords UpdateDiseaseRecordByDiseaseRecordId(int id, [FromBody] DiseaseRecords model)
        {
            return _doctorPatientManagementService.UpdateDiseaseRecordByDiseaseRecordId(id, model);
        }
    }
}