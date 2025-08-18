using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
