using Application.DTOs.Assessment;
using Application.Exceptions;
using Application.UseCasesInterfaces.AssessmentUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AssessmentUseCases
{
    public class StartAssessmentUseCase : IStartAssessmentUseCase
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IAssessmentSessionRepository _assessmentSessionRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateAnswerRepository _candidateAnswerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StartAssessmentUseCase(
            IAssessmentRepository assessmentRepository,
            IAssessmentSessionRepository assessmentSessionRepository,
            IApplicationRepository applicationRepository,
            ICandidateAnswerRepository candidateAnswerRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _assessmentRepository = assessmentRepository;
            _assessmentSessionRepository = assessmentSessionRepository;
            _applicationRepository = applicationRepository;
            _candidateAnswerRepository = candidateAnswerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AssessmentSessionResponseDTO> ExecuteAsync(StartAssessmentDTO startAssessmentDTO)
        {
            var application = await _applicationRepository.GetByIdAsync(startAssessmentDTO.ApplicationId);
            if (application == null)
            {
                throw new NotFoundException("Application", startAssessmentDTO.ApplicationId);
            }

            var assessment = await _assessmentRepository.GetAssessmentForCandidateAsync(
                startAssessmentDTO.AssessmentId, startAssessmentDTO.ApplicationId);
            
            if (assessment == null)
            {
                throw new NotFoundException("Assessment not found or not available for this application");
            }

            var existingSession = await _assessmentSessionRepository.GetActiveSessionAsync(
                startAssessmentDTO.ApplicationId, startAssessmentDTO.AssessmentId);

            if (existingSession != null)
            {
                throw new InvalidOperationException("You have already started this assessment and cannot re-enter.");
            }

            var session = _mapper.Map<AssessmentSession>(startAssessmentDTO);
            session.StartTime = DateTime.UtcNow;
            session.IsSubmitted = false;
            session.CreatedAt = DateTime.UtcNow;

            await _assessmentSessionRepository.AddAsync(session);
            await _unitOfWork.SaveChangesAsync();

            var newSession = await _assessmentSessionRepository.GetSessionWithAnswersAsync(session.Id);
            if (newSession == null)

            {
                throw new Exception("Failed to create assessment session.");
            }
            var response = _mapper.Map<AssessmentSessionResponseDTO>(newSession);
            var currentTime = DateTime.UtcNow;
            var expectedEndTime = session.StartTime.AddSeconds(newSession.Assessment.TotalDuration);
            response.ExpectedEndTime = expectedEndTime;
            response.RemainingTime = Math.Max(0, (int)(expectedEndTime - currentTime).TotalSeconds);
            return response;
        }
    }
}