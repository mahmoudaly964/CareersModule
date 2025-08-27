using Application.DTOs.Assessment;

namespace Application.UseCasesInterfaces.AssessmentUseCase
{
    public interface IStartAssessmentUseCase
    {
        Task<AssessmentSessionResponseDTO> ExecuteAsync(StartAssessmentDTO startAssessmentDTO);
    }
}