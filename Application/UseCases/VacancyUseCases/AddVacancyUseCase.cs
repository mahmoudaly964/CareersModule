using Application.DTOs.Vacancy;
using Application.UseCasesInterfaces.Vacancy;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.VacancyUseCases
{
    public class AddVacancyUseCase : IAddVacancyUseCase
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddVacancyUseCase(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task ExecuteAsync(AddVacancyDTO newVacancy)
        {
            if (newVacancy == null)
            {
                throw new ArgumentNullException(nameof(newVacancy), "Vacancy data cannot be null.");
            }
            
            var vacancy = _mapper.Map<Vacancy>(newVacancy);
            await _vacancyRepository.AddAsync(vacancy);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
