using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZdravstveniKartoni.Dtos;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Services;

namespace ZdravstveniKartoni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private INursesService _nursesService;

        public NursesController(INursesService nursesService)
        {
            _nursesService = nursesService;
        }

        /// <summary>
        /// get appointments
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAppointments")]
        public List<AppointmentsDto> GetAppointments()
        {
            return _nursesService.GetAppointments();
        }

        /// <summary>
        /// approve appointment for patient by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("approveAppointment/{userId}")]
        public bool ApproveAppointment(int userId)
        {
            return _nursesService.ApproveAppointment(userId);
        }

        /// <summary>
        /// decline appointment for patient by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("declineAppointment/{userId}")]
        public bool DeclineAppointment(int userId)
        {
            return _nursesService.DeclineAppointment(userId);
        }

        [HttpGet("getApprovedAppointments")]
        public List<AppointmentsDto> GetApprovedAppointments()
        {
            return _nursesService.GetApprovedAppointments();
        }

        [HttpGet("getDeclinedAppointments")]
        public List<AppointmentsDto> GetDeclinedAppointments()
        {
            return _nursesService.GetDeclinedAppointments();
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpPost("searchAppointments")]
        public List<AppointmentsDto> SearchAppointments([FromQuery] string searchValue)
        {
            return _nursesService.SearchAppointments(searchValue);
        }

        /// <summary>
        /// Search approved
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpPost("searchApprovedAppointments")]
        public List<AppointmentsDto> SearchApprovedAppointments([FromQuery] string searchValue)
        {
            return _nursesService.SearchApprovedAppointments(searchValue);
        }
    }
}