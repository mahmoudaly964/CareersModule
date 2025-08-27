using Application.DTOs.Interview;
using Application.Exceptions;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.InterviewUseCase;
using Domain.Interfaces;

namespace Application.UseCases.InterviewUseCases
{
    public class CancelInterviewUseCase : ICancelInterviewUseCase
    {
        private readonly IInterviewRepository _interviewRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public CancelInterviewUseCase(IInterviewRepository interviewRepository, IUnitOfWork unitOfWork, IEmailService emailService, IApplicationRepository applicationRepository)
        {
            _interviewRepository = interviewRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _applicationRepository = applicationRepository;
        }

        public async Task ExecuteAsync(CancelInterviewDTO dto)
        {
            var interview = await _interviewRepository.GetByIdAsync(dto.InterviewId);
            if (interview == null)
                throw new NotFoundException("Interview", dto.InterviewId);

            var candidateEmail = await _applicationRepository.GetCandidateEmailByApplicationId(interview.ApplicationId);
            if (string.IsNullOrEmpty(candidateEmail))
                throw new NotFoundException("Candidate email not found for ApplicationId", interview.ApplicationId);
            await _emailService.SendInterviewCanceledEmail(candidateEmail, interview.ScheduledDate, interview.MeetingLink);

            await _interviewRepository.DeleteAsync(interview.Id);
            await _unitOfWork.SaveChangesAsync();

            
        }
    }
}