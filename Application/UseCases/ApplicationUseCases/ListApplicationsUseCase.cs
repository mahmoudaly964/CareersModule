using Application.DTOs.Application;
using Application.UseCasesInterfaces.ApplicationUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq.Expressions;

namespace Application.UseCases.ApplicationUseCases
{
    public class ListApplicationsUseCase : IListApplicationsUseCase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ListApplicationsUseCase(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationResponseDTO>> ExecuteAsync(
            string? status, 
            Guid? vacancyId, 
            Guid? candidateId,
            string? candidateName,
            string? vacancyTitle,
            int pageNumber = 1, 
            int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize > 100) pageSize = 100;

            Expression<Func<Domain.Entities.Application, bool>>? filter = null;

            // Filter by status
            if (!string.IsNullOrEmpty(status))
            {
                var lowerStatus = status.ToLower();
                filter = a => a.Status.ToLower().Contains(lowerStatus);
            }

            // Filter by vacancy ID
            else if (vacancyId.HasValue)
            {
                filter = a => a.VacancyId == vacancyId.Value;
            }

            // Filter by candidate ID
            else if (candidateId.HasValue)
            {
                filter = a => a.CandidateId == candidateId.Value;
            }


            // Filter by candidate name (search in User.FullName)
            else if (!string.IsNullOrEmpty(candidateName))
            {
                var lowerCandidateName = candidateName.ToLower();
                filter = a => a.Candidate != null && 
                                        a.Candidate.User != null && 
                                        a.Candidate.User.FullName.ToLower().Contains(lowerCandidateName);
            }

            // Filter by vacancy title
            else if (!string.IsNullOrEmpty(vacancyTitle))
            {
                var lowerVacancyTitle = vacancyTitle.ToLower();
                filter = a => a.Vacancy != null && 
                                        a.Vacancy.Title.ToLower().Contains(lowerVacancyTitle);
            }
            

            var applications = await _applicationRepository.GetAllAsync(
                filter, 
                includeProperties: "Candidate,Candidate.User,Vacancy",
                pageNumber: pageNumber, 
                pageSize: pageSize
                );

            return _mapper.Map<IEnumerable<ApplicationResponseDTO>>(applications);
        }
    }
    
}