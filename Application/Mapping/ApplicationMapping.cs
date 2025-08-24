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
            CreateMap<AddApplicationDTO, Candidate>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
              .ForMember(dest => dest.User, opt => opt.Ignore())
              .ForMember(dest => dest.Applications, opt => opt.Ignore());

           
            CreateMap<AddApplicationDTO, Domain.Entities.Application>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CandidateId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Candidate, opt => opt.Ignore())
                .ForMember(dest => dest.Vacancy, opt => opt.Ignore())
                .ForMember(dest => dest.AssessmentSessions, opt => opt.Ignore())
                .ForMember(dest => dest.Interviews, opt => opt.Ignore());
        
        }
    }
}