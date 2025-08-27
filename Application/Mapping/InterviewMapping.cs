using Application.DTOs.Auth;
using Application.DTOs.Interview;
using Application.DTOs.InterviewFeedback;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class InterviewMapping:Profile
    {
        public InterviewMapping() 
        {
            CreateMap<Interview, ScheduleInterviewDTO>().ReverseMap();
            CreateMap<InterviewFeedback, AddInterviewFeedbackDTO>().ReverseMap();

        }
    }
}
