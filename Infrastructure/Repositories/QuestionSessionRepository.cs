using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class QuestionSessionRepository : Repository<QuestionSession>, IQuestionSessionRepository
    {
        public QuestionSessionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<QuestionSession?> GetQuestionSessionAsync(Guid assessmentSessionId, Guid questionId)
        {
            return await _context.QuestionSessions
                .FirstOrDefaultAsync(qs => qs.AssessmentSessionId == assessmentSessionId && qs.QuestionId == questionId);
        }
    }
}