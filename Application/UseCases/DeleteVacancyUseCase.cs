using Application.Exceptions;
using Application.UseCasesInterfaces.Vacancy;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class DeleteVacancyUseCase : IDeleteVacancyUseCase
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteVacancyUseCase(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork)
        {
            _vacancyRepository = vacancyRepository ;
            _unitOfWork = unitOfWork; 
        }
        public async Task ExecuteAsync(Guid vacancyId)
        {
            if(vacancyId == Guid.Empty)
            {
                throw new ArgumentException("Vacancy ID cannot be empty.", nameof(vacancyId));
            }
            var vacancy= _vacancyRepository.GetByIdAsync(vacancyId);
            if (vacancy == null)
            {
                throw new NotFoundException("Vacancy", vacancyId);
            }
            await _vacancyRepository.DeleteAsync(vacancyId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
