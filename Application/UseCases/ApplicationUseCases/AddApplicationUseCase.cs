using Application.DTOs.Application;
using Application.Exceptions;
using Application.UseCasesInterfaces.ApplicationUseCase;
using AutoMapper;
using Domain.Interfaces;
using Domain.Entities;
using Application.Services_Interfaces;

namespace Application.UseCases.ApplicationUseCases
{
    public class AddApplicationUseCase : IAddApplicationUseCase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddApplicationUseCase(
            IApplicationRepository applicationRepository,
            ICandidateRepository candidateRepository,
            IVacancyRepository vacancyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,IEmailService emailService)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task ExecuteAsync(AddApplicationDTO applicationDTO)
        {
            if (applicationDTO == null)
            {
                throw new ArgumentNullException(nameof(applicationDTO), "Application data cannot be null.");
            }

            var vacancy = await _vacancyRepository.GetByIdAsync(applicationDTO.VacancyId);
            if (vacancy == null || !vacancy.IsPublished)
            {
                throw new NotFoundException("Vacancy", applicationDTO.VacancyId);
            }

            var existingCandidates = await _candidateRepository.GetAllAsync(
                c => c.UserId == applicationDTO.UserId);

            if (existingCandidates.Any())
            {
                var existingApplications = await _applicationRepository.GetAllAsync(
                    a => existingCandidates.Select(c => c.Id).Contains(a.CandidateId) &&
                         a.VacancyId == applicationDTO.VacancyId);

                //if (existingApplications.Any())
                //{
                //    throw new InvalidOperationException("You have already applied for this job");
                //}
            }

            var candidate = _mapper.Map<Candidate>(applicationDTO);

            await _candidateRepository.AddAsync(candidate);
            await _unitOfWork.SaveChangesAsync();
            var application =_mapper.Map<Domain.Entities.Application>(applicationDTO);
            application.CandidateId = candidate.Id;

            await _applicationRepository.AddAsync(application);
            await _unitOfWork.SaveChangesAsync();
            var email = await _applicationRepository.GetCandidateEmailByApplicationId(application.Id);   
            var name = await _applicationRepository.GetCandidateNameByApplicationId(application.Id);
            var vacancyTitle =await _applicationRepository.GetVacancyTitleByApplicationId(application.Id);

            if (email !=null && name!=null&&vacancyTitle!=null )
            {
                await _emailService.SendApplicationConfirmationEmail(email, name , vacancyTitle);
            }
        }
    }
}