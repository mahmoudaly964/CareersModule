using Application.DTOs.InterviewFeedback;

namespace Application.Services_Interfaces
{
    public interface IInterviewFeedbackService
    {
        Task<Guid> AddFeedbackAsync(AddInterviewFeedbackDTO dto);
    }
}