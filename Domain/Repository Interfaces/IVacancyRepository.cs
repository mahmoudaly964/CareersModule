using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository_Interfaces
{
    public interface IVacancyRepository
    {
        public Task UnPublishVacancy(Guid VacancyId);
        public Task PublishVacancy(Guid VacancyId);
        Task<IEnumerable<Vacancy>> SearchAndFilterAsync(string? role,bool? isPublished,int pageNumber,
                                                                                         int pageSize);
    }
}
