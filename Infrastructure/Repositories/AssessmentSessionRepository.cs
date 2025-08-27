using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssessmentSessionRepository : Repository<AssessmentSession>, IAssessmentSessionRepository
    {
        public AssessmentSessionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<AssessmentSession?> GetActiveSessionAsync(Guid applicationId, Guid assessmentId)
        {
            return await _context.AssessmentSessions
                .Include(s => s.Assessment)
                .Include(s => s.Application)
                .FirstOrDefaultAsync(s => s.ApplicationId == applicationId && 
                                        s.AssessmentId == assessmentId && 
                                        !s.IsSubmitted);
        }

        public async Task<AssessmentSession?> GetSessionWithAnswersAsync(Guid sessionId)
        {
            return await _context.AssessmentSessions
                .Include(s => s.Assessment)
                    .ThenInclude(a => a.Questions)
                        .ThenInclude(q => q.Options)
                .Include(s => s.Answers)
                    .ThenInclude(a => a.Question)
                .Include(s => s.Answers)
                    .ThenInclude(a => a.SelectedOption)
                .Include(s => s.Application)
                .FirstOrDefaultAsync(s => s.Id == sessionId);
        }
    }
}