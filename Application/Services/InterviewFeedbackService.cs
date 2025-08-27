using Application.DTOs.InterviewFeedback;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.InterviewFeedback;

namespace Application.Services
{
    public class InterviewFeedbackService : IInterviewFeedbackService
    {
        private readonly IAddInterviewFeedbackUseCase _addInterviewFeedbackUseCase;

        public InterviewFeedbackService(IAddInterviewFeedbackUseCase addInterviewFeedbackUseCase)
        {
            _addInterviewFeedbackUseCase = addInterviewFeedbackUseCase;
        }

        public async Task<Guid> AddFeedbackAsync(AddInterviewFeedbackDTO dto)
        {
            return await _addInterviewFeedbackUseCase.ExecuteAsync(dto);
        }
    }
}