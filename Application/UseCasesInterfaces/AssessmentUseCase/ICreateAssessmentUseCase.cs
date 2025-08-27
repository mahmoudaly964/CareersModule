using Application.DTOs.Assessment;

namespace Application.UseCasesInterfaces.AssessmentUseCase
{
    public interface ICreateAssessmentUseCase
    {
        Task<Guid> ExecuteAsync(CreateAssessmentDTO createAssessmentDTO);
    }
}