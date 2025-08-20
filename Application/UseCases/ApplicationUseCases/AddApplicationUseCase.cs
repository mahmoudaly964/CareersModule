using Application.DTOs.Application;
using Application.Exceptions;
using Application.UseCasesInterfaces.ApplicationUseCase;
using AutoMapper;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.UseCases.ApplicationUseCases
{
    public class AddApplicationUseCase : IAddApplicationUseCase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddApplicationUseCase(
            IApplicationRepository applicationRepository,
            ICandidateRepository candidateRepository,
            IVacancyRepository vacancyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(AddApplicationDTO applicationDTO)
        {
            if (applicationDTO == null)
            {
                throw new ArgumentNullException(nameof(applicationDTO), "Application data cannot be null.");
            }

            var candidate = await _candidateRepository.GetByIdAsync(applicationDTO.CandidateId);
            if (candidate == null)
            {
                throw new NotFoundException("Candidate", applicationDTO.CandidateId);
            }

            // Verify vacancy exists and is published
            var vacancy = await _vacancyRepository.GetByIdAsync(applicationDTO.VacancyId);
            if (vacancy == null || !vacancy.IsPublished)
            {
                throw new NotFoundException("Vacancy", applicationDTO.VacancyId);
            }

            // Check if application already exists
            var existingApplications = await _applicationRepository.GetAllAsync(
                a => a.CandidateId == applicationDTO.CandidateId && a.VacancyId == applicationDTO.VacancyId);

            if (existingApplications.Any())
            {
                throw new InvalidOperationException("You have already applied for this job");
            }

            var application = _mapper.Map<Domain.Entities.Application>(applicationDTO);

            application.Status = "Pending";

            await _applicationRepository.AddAsync(application);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}