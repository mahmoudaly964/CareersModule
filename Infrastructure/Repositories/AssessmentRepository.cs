using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssessmentRepository : Repository<Assessment>, IAssessmentRepository
    {
        public AssessmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Assessment?> GetAssessmentAsync(Guid assessmentId)
        {
            return await _context.Assessments
                .Include(a => a.Questions)
                    .ThenInclude(q => q.Options)
                .Include(a => a.Vacancy)
                .FirstOrDefaultAsync(a => a.Id == assessmentId);
        }

        public async Task<Assessment?> GetAssessmentForCandidateAsync(Guid assessmentId, Guid applicationId)
        {
            return await _context.Assessments
                .Include(a => a.Questions.OrderBy(q => q.CreatedAt))
                    .ThenInclude(q => q.Options)
                .Include(a => a.Vacancy)
                .Where(a => a.Id == assessmentId &&
                           a.IsActive &&
                           a.Deadline > DateTime.UtcNow &&
                           a.Vacancy.Applications.Any(app => app.Id == applicationId))
                .FirstOrDefaultAsync();
        }
    }
}