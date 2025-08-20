using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}