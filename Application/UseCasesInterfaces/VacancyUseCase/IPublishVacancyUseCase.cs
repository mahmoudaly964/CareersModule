using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.VacancyUseCase
{
    public interface IPublishVacancyUseCase
    {
        public Task ExecuteAsync(Guid vacancyId);

    }
}
