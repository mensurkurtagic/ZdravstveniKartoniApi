using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravstveniKartoni.Dtos;
using ZdravstveniKartoni.Entities;
using ZdravstveniKartoni.Helpers;

namespace ZdravstveniKartoni.Services
{
    public interface IRegisterUsersService
    {
        bool RegisterDoctor([FromBody] RegisterUsersDto model);
        bool RegisterNurse([FromBody] RegisterUsersDto model);
        bool RegisterPharmacy([FromBody] RegisterUsersDto model);
    }
    public class RegisterUsersService : IRegisterUsersService
    {
        private IUserService _userService;
        private DataContext _dataContext;
        private IMapper _mapper;

        public RegisterUsersService(IUserService userService, DataContext dataContext, IMapper mapper)
        {
            _userService = userService;
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public bool RegisterDoctor(RegisterUsersDto model)
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

                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
        }

        public bool RegisterNurse([FromBody] RegisterUsersDto model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                _userService.Create(user, model.Password);

                _dataContext.UserRoles.Add(new UserRoles()
                {
                    RoleId = 3,
                    UserId = user.Id
                });
                _dataContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
        }

        public bool RegisterPharmacy([FromBody] RegisterUsersDto model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                _userService.Create(user, model.Password);

                _dataContext.UserRoles.Add(new UserRoles()
                {
                    RoleId = 4,
                    UserId = user.Id
                });
                _dataContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
        }
    }
}
