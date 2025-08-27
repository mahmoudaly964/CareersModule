using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class InterviewFeedbackRepository : Repository<InterviewFeedback>, IInterviewFeedbackRepository
    {
        public InterviewFeedbackRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}