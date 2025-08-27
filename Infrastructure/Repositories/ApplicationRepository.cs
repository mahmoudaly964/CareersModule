using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {

        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<string?> GetCandidateEmailByApplicationId(Guid applicationId)
        {
            return await _context.Applications
                .Where(a => a.Id == applicationId)
                .Include(a => a.Candidate)
                    .ThenInclude(c => c.User)
                .Select(a => a.Candidate.User.Email)
                .FirstOrDefaultAsync();
        }
        public async Task<string?> GetCandidateNameByApplicationId(Guid applicationId)
        {
            return await _context.Applications
                .Where(a => a.Id == applicationId)
                .Include(a => a.Candidate)
                    .ThenInclude(c => c.User)
                .Select(a => a.Candidate.User.FullName)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetVacancyTitleByApplicationId(Guid applicationId)
        {
            return await _context.Applications
                .Where(a => a.Id == applicationId)
                .Include(a => a.Vacancy)
                .Select(a => a.Vacancy.Title)
                .FirstOrDefaultAsync();
        }
    }
}