using Application.DTOs.Application;
using Application.Exceptions;
using Application.UseCasesInterfaces.ApplicationUseCase;
using Domain.Interfaces;

namespace Application.UseCases.ApplicationUseCases
{
    public class UpdateApplicationStatusUseCase : IUpdateApplicationStatusUseCase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateApplicationStatusUseCase(IApplicationRepository applicationRepository, IUnitOfWork unitOfWork)
        {
            _applicationRepository = applicationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid applicationId, UpdateApplicationStatusDTO statusDTO)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application == null)
            {
                throw new NotFoundException("Application", applicationId);
            }

            var validStatuses = new[] { "Pending", "Shortlisted", "Rejected", "Hired" };
            if (!validStatuses.Contains(statusDTO.Status))
            {
                throw new ArgumentException($"Invalid status. Valid statuses are: {string.Join(", ", validStatuses)}");
            }

            application.Status = statusDTO.Status;
            await _applicationRepository.UpdateAsync(application);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}