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
    public class RegisterUsersController : ControllerBase
    {
        private IRegisterUsersService _registerUsersService;

        public RegisterUsersController(IRegisterUsersService registerUsersService)
        {
            _registerUsersService = registerUsersService;
        }

        /// <summary>
        /// Register Doctor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("registerDoctor")]
        public IActionResult RegisterDoctor([FromBody] RegisterUsersDto model)
        {
            if (_registerUsersService.RegisterDoctor(model))
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Register Nurse
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("registerNurse")]
        public IActionResult RegisterNurse([FromBody] RegisterUsersDto model)
        {
            if (_registerUsersService.RegisterNurse(model))
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Register Doctor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("registerPharmacy")]
        public IActionResult RegisterPharmacy([FromBody] RegisterUsersDto model)
        {
            if (_registerUsersService.RegisterPharmacy(model))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}