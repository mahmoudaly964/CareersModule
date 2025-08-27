using Application.DTOs.Auth;
using Application.DTOs.Vacancy;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<SignupDTO, ApplicationUser>().ReverseMap();
            CreateMap<LoginDTO, ApplicationUser>().ReverseMap();
            CreateMap<AuthResponseDTO, ApplicationUser>().ReverseMap();
        }
    }
}
