using Application.DTOs.Assessment;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class AssessmentMapping : Profile
    {
        public AssessmentMapping()
        {
            CreateMap<Assessment, AssessmentResponseDTO>()
                .ForMember(dest => dest.VacancyTitle, opt => opt.MapFrom(src => src.Vacancy.Title))
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
                .ReverseMap();

            CreateMap<Assessment, CreateAssessmentDTO>().ReverseMap();

            CreateMap<Question, QuestionResponseDTO>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options))
                .ReverseMap();

            CreateMap<QuestionOption, QuestionOptionResponseDTO>().ReverseMap();

            CreateMap<Question, CreateQuestionDTO>().ReverseMap();

            CreateMap<QuestionOption, CreateQuestionOptionDTO>().ReverseMap();

            CreateMap<AssessmentSession, AssessmentSessionResponseDTO>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Assessment.Questions))
                .ForMember(dest => dest.AssessmentTitle, opt => opt.MapFrom(src => src.Assessment.Title))
                .ForMember(dest => dest.TotalDuration, opt => opt.MapFrom(src => src.Assessment.TotalDuration))
                .ReverseMap();

            CreateMap<Question, QuestionForCandidateDTO>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options))
                .ReverseMap();

            CreateMap<QuestionOption, OptionForCandidateDTO>().ReverseMap();

            CreateMap<CandidateAnswer, CandidateAnswerDTO>().ReverseMap();
            CreateMap<StartAssessmentDTO, AssessmentSession>().ReverseMap();
            CreateMap<StartQuestionDTO, QuestionSession>().ReverseMap();
            CreateMap<SubmitAnswerDTO, CandidateAnswer>().ReverseMap();
            CreateMap<SubmitAssessmentDTO, AssessmentSession>().ReverseMap();

        }
    }
}