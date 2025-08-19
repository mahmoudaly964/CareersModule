using Application.DTOs.Vacancy;
using Application.UseCasesInterfaces.VacancyUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.VacancyUseCases
{
    public class ListVacancyUseCase : IListVacancyUseCase
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IMapper _mapper;

        public ListVacancyUseCase(IVacancyRepository vacancyRepository, IMapper mapper)
        {
            _vacancyRepository = vacancyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VacancyResponseDTO>> ExecuteAsync(string? role, int pageNumber=1, int pageSize=10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if ( pageSize > 100) pageSize = 100;
            Expression<Func<Vacancy, bool>>? filter = null;
            if (!string.IsNullOrEmpty(role))
            {
                var loweredRole = role.ToLower();

                filter = v => v.Role.ToLower().Contains(loweredRole) && v.IsPublished;
            }
            var vacancies = await _vacancyRepository.GetAllAsync(filter,pageNumber: pageNumber, pageSize: pageSize);

            return _mapper.Map<IEnumerable<VacancyResponseDTO>>(vacancies);
        }
    }
}