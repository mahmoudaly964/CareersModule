using Application.DTOs.Vacancy;
using Application.Exceptions;
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
    public class UpdateVacancyUseCase : IUpdateVacancyUseCase
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateVacancyUseCase(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task ExecuteAsync(UpdateVacancyDTO updateVacancyDTO,Guid vacancyId)
        {
            var existingVacancy = await _vacancyRepository.GetByIdAsync(vacancyId);
            if(existingVacancy == null)
            {
                throw new NotFoundException("Vacancy", vacancyId);
            }
            _mapper.Map( updateVacancyDTO, existingVacancy);
            await _vacancyRepository.UpdateAsync(existingVacancy);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
