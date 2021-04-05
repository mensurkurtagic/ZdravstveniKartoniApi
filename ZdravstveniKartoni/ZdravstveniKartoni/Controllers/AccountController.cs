using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ZdravstveniKartoni.Dtos;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Helpers;
using ZdravstveniKartoni.Services;

namespace ZdravstveniKartoni.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private DataContext _dataContext;

        public AccountController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings, DataContext dataContext)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _dataContext = dataContext;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            try
            {
                var user = _userService.Authenticate(userDto.Username, userDto.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password incorrect!" });

                var userRoleName = _userService.GetRoleByUser(user);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, userRoleName),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    Id = user.Id,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = userRoleName,
                    Token = tokenString
                });
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                _userService.Create(user, userDto.Password);

                _dataContext.Patients.Add(new Patients()
                {
                    RoleId = 2,
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = "",
                    Gender = "",
                    JMBG = "",
                    BloodType = "",
                    RHFaktor = "",
                    Address = ""
                });
                _dataContext.SaveChanges();

                var patientId = _dataContext.Patients.Where(p => p.UserId == user.Id).FirstOrDefault().Id;

                _dataContext.DoctorsPatients.Add(new DoctorsPatients()
                {
                    DoctorId = userDto.DoctorId,
                    PatientId = patientId
                });

                _dataContext.SaveChanges();

                _dataContext.UserRoles.Add(new UserRoles()
                {
                    RoleId = 2,
                    UserId = user.Id
                });
                _dataContext.SaveChanges();

                return Ok(user.Id);
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetUserById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        /// <summary>
        /// Update user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;
            try
            {
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("registerDoctor")]
        public IActionResult RegisterDoctor([FromBody] RegisterUsersDto model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                _userService.Create(user, model.Password);

                _dataContext.UserRoles.Add(new UserRoles()
                {
                    RoleId = 1,
                    UserId = user.Id
                });
                _dataContext.SaveChanges();

                return Ok(user.Id);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}