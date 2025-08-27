using Application.DTOs.Assessment;

namespace Application.Services_Interfaces
{
    public interface IAssessmentService
    {
        Task<Guid> CreateAssessmentAsync(CreateAssessmentDTO createAssessmentDTO);
        Task<AssessmentResponseDTO> GetAssessmentAsync(Guid assessmentId);
        Task<AssessmentSessionResponseDTO> StartAssessmentAsync(StartAssessmentDTO startAssessmentDTO);
        Task SubmitAnswerAsync(SubmitAnswerDTO submitAnswerDTO);
        Task SubmitAssessmentAsync(SubmitAssessmentDTO submitAssessmentDTO);
        Task<QuestionResponseDTO> StartQuestionAsync(StartQuestionDTO startQuestionDTO);

    }
}