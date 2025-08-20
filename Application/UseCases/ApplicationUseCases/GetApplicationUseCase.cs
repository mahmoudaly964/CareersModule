using Application.DTOs.Application;
using Application.Exceptions;
using Application.UseCasesInterfaces.ApplicationUseCase;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.ApplicationUseCases
{
    public class GetApplicationUseCase : IGetApplicationUseCase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public GetApplicationUseCase(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationResponseDTO> ExecuteAsync(Guid applicationId)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);

            if (application == null)
            {
                throw new NotFoundException("Application", applicationId);
            }

            return _mapper.Map<ApplicationResponseDTO>(application);
        }
    }
}