using Application.DTOs.Interview;
using Application.Exceptions;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.InterviewUseCase;
using Domain.Interfaces;

namespace Application.UseCases.InterviewUseCases
{
    public class RescheduleInterviewUseCase : IRescheduleInterviewUseCase
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public RescheduleInterviewUseCase(IInterviewRepository interviewRepository, IUnitOfWork unitOfWork, IEmailService emailService, IApplicationRepository applicationRepository)
        {
            _interviewRepository = interviewRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _applicationRepository = applicationRepository;
        }

        public async Task ExecuteAsync(RescheduleInterviewDTO dto)
        {
            var interview = await _interviewRepository.GetByIdAsync(dto.InterviewId);
            if (interview == null)
                throw new NotFoundException("Interview", dto.InterviewId);

            interview.ScheduledDate = dto.NewScheduledDate;
            interview.MeetingLink = dto.NewMeetingLink;
            interview.Status = "Rescheduled";
            await _interviewRepository.UpdateAsync(interview);
            await _unitOfWork.SaveChangesAsync();

            var candidateEmail = await _applicationRepository.GetCandidateEmailByApplicationId(interview.ApplicationId);

            if (string.IsNullOrEmpty(candidateEmail))
                throw new NotFoundException("Candidate email not found for ApplicationId", interview.ApplicationId);

            await _emailService.SendInterviewRescheduledEmail(candidateEmail, interview.ScheduledDate, interview.MeetingLink);
        }
    }
}