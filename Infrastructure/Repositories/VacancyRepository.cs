using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VacancyRepository : Repository<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task PublishVacancy(Guid vacancyId)
        {
            var vacancy = await GetByIdAsync(vacancyId,tracking:true);
            if (vacancy != null)
            {
                vacancy.IsPublished = true;
                await UpdateAsync(vacancy);
            }
        }

        public async Task UnPublishVacancy(Guid vacancyId)
        {
            var vacancy = await GetByIdAsync(vacancyId);
            if (vacancy != null)
            {
                vacancy.IsPublished = false;
                await UpdateAsync(vacancy);
            }
        }
        public  async Task<IEnumerable<Vacancy>> GetAllPublishedAsync(Expression<Func<Vacancy, bool>>? filter = null,
                                                                    int? pageNumber = 1,
                                                                    int? pageSize = 10,
                                                                    bool tracking = true)
        {
            IQueryable<Vacancy> query = tracking ? _dbSet : _dbSet.AsNoTracking();

            query = query.Where(v => v.IsPublished);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                           .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }
    }
}
