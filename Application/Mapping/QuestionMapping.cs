using Application.DTOs.Question;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class QuestionMapping : Profile
    {
        public QuestionMapping()
        {
            CreateMap<AddQuestionDTO, Question>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options))
                .ReverseMap();

            CreateMap<AddQuestionOptionDTO, QuestionOption>().ReverseMap();

            CreateMap<UpdateQuestionDTO, Question>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options))
                .ReverseMap();

            CreateMap<UpdateQuestionOptionDTO, QuestionOption>().ReverseMap();
        }
    }
}