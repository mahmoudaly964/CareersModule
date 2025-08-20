using Application.DTOs.Application;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<AddApplicationDTO, Domain.Entities.Application>().ReverseMap();
            
            CreateMap<Domain.Entities.Application, ApplicationResponseDTO>()
                .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate != null ? src.Candidate.User.FullName : null))
                .ForMember(dest => dest.VacancyTitle, opt => opt.MapFrom(src => src.Vacancy != null ? src.Vacancy.Title : null));
        }
    }
}