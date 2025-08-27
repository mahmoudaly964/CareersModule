using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<Question?> GetByIdAsync(Guid id)
        
        {
            return await _context.Questions
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}