using Application.DTOs.Assessment;
using Application.Exceptions;
using Application.UseCasesInterfaces.AssessmentUseCase;
using Domain.Interfaces;

namespace Application.UseCases.AssessmentUseCases
{
    public class SubmitAssessmentUseCase : ISubmitAssessmentUseCase
    {
        private readonly IAssessmentSessionRepository _assessmentSessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitAssessmentUseCase(
            IAssessmentSessionRepository assessmentSessionRepository,
            IUnitOfWork unitOfWork)
        {
            _assessmentSessionRepository = assessmentSessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(SubmitAssessmentDTO submitAssessmentDTO)
        {
            var session = await _assessmentSessionRepository.GetByIdAsync(submitAssessmentDTO.AssessmentSessionId);
            if (session == null)
            {
                throw new NotFoundException("Assessment session", submitAssessmentDTO.AssessmentSessionId);
            }

            if (session.IsSubmitted)
            {
                throw new InvalidOperationException("Assessment has already been submitted");
            }
            var expectedEndTime = session.ExpectedEndTime;
            if (DateTime.UtcNow > expectedEndTime)
            {
                session.IsSubmitted = true;
                session.EndTime = expectedEndTime;
                throw new InvalidOperationException("Assessment time has expired, your answers before deadline have been auto submitted");
            }

            session.IsSubmitted = true;
            session.EndTime = DateTime.UtcNow;

            await _assessmentSessionRepository.UpdateAsync(session);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}