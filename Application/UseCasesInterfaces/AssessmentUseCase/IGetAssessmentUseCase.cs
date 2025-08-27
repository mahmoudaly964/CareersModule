using Application.DTOs.Assessment;

namespace Application.UseCasesInterfaces.AssessmentUseCase
{
    public interface IGetAssessmentUseCase
    {
        Task<AssessmentResponseDTO> ExecuteAsync(Guid assessmentId);
    }
}