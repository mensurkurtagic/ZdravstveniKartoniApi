using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravstveniKartoni.Dtos;
using ZdravstveniKartoni.Entities;

namespace ZdravstveniKartoni.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<RegisterUsersDto, User>();
        }
    }
}
