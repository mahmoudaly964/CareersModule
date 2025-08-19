using Application.DTOs.Vacancy;
using Application.Exceptions;
using Application.UseCasesInterfaces.VacancyUseCase;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetVacancyUseCase : IGetVacancyUseCase
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IMapper _mapper;

        public GetVacancyUseCase(IVacancyRepository vacancyRepository, IMapper mapper)
        {
            _vacancyRepository = vacancyRepository;
            _mapper = mapper;
        }

        public async Task<VacancyResponseDTO> ExecuteAsync(Guid vacancyId)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);

            if (vacancy == null)
            {
                throw new NotFoundException("Vacancy", vacancyId);
            }

            return _mapper.Map<VacancyResponseDTO>(vacancy);
        }
    }
}