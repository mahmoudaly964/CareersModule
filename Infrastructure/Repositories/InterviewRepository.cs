using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InterviewRepository : Repository<Interview>, IInterviewRepository
    {
        public InterviewRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}