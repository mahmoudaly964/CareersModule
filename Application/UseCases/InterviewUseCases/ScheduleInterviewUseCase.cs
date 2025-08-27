using Application.DTOs.Interview;
using Application.Exceptions;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.InterviewUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.InterviewUseCases
{
    public class ScheduleInterviewUseCase : IScheduleInterviewUseCase
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IApplicationRepository _applicationRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ScheduleInterviewUseCase(IInterviewRepository interviewRepository, IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, IApplicationRepository applicationRepository)
        {
            _interviewRepository = interviewRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _applicationRepository = applicationRepository;
        }

        public async Task<Guid> ExecuteAsync(ScheduleInterviewDTO dto)
        {
            var interview = _mapper.Map<Interview>(dto);
            interview.Status = "Scheduled";
            await _interviewRepository.AddAsync(interview);
            await _unitOfWork.SaveChangesAsync();

            
            var candidateEmail = await _applicationRepository.GetCandidateEmailByApplicationId(dto.ApplicationId);
            if (string.IsNullOrEmpty(candidateEmail))
                throw new NotFoundException("Candidate email not found for ApplicationId", interview.ApplicationId);
            await _emailService.SendInterviewScheduledEmail(candidateEmail, interview.ScheduledDate, interview.MeetingLink);

            return interview.Id;
        }
    }
}