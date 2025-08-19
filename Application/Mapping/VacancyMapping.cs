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
    public class VacancyMapping : Profile
    {
        public VacancyMapping()
        {
            CreateMap<Vacancy, VacancyResponseDTO>().ReverseMap();
            CreateMap<Vacancy, AddVacancyDTO>().ReverseMap();
            CreateMap<Vacancy, UpdateVacancyDTO>().ReverseMap();
        }
    }
}
