using Application.DTOs.Vacancy;
using Application.UseCasesInterfaces.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class AddVacancyUseCase : IAddVacancyUseCase
    {

        public async Task<bool> ExcuteAsync(AddVacancyDTO newVacancy)
        {
            if (newVacancy == null)
            {
                return await Task.FromResult(false);  
            }
            return true;
        }
    }
}
