using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CandidateAnswerRepository : Repository<CandidateAnswer>, ICandidateAnswerRepository
    {
        public CandidateAnswerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CandidateAnswer?> GetAnswerAsync(Guid sessionId, Guid questionId)
        {
            return await _context.CandidateAnswers
                .Include(a => a.SelectedOption)
                .FirstOrDefaultAsync(a => a.AssessmentSessionId == sessionId && a.QuestionId == questionId);
        }

        public async Task<IEnumerable<CandidateAnswer>> GetSessionAnswersAsync(Guid sessionId)
        {
            return await _context.CandidateAnswers
                .Include(a => a.Question)
                .Include(a => a.SelectedOption)
                .Where(a => a.AssessmentSessionId == sessionId)
                .ToListAsync();
        }
    }
}