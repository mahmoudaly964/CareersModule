using Application.DTOs.Assessment;

namespace Application.UseCasesInterfaces.AssessmentUseCase
{
    public interface ISubmitAssessmentUseCase
    {
        Task ExecuteAsync(SubmitAssessmentDTO submitAssessmentDTO);
    }
}