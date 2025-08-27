using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IQuestionSessionRepository : IRepository<QuestionSession>
    {
        Task<QuestionSession?> GetQuestionSessionAsync(Guid assessmentSessionId, Guid questionId);
    }
}