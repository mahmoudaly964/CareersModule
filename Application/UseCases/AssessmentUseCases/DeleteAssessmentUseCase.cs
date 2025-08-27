using Application.DTOs.Assessment;
using Application.Exceptions;
using Application.UseCasesInterfaces.AssessmentUseCase;
using Domain.Interfaces;

namespace Application.UseCases.AssessmentUseCases
{
    public class DeleteAssessmentUseCase : IDeleteAssessmentUseCase
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAssessmentUseCase(IAssessmentRepository assessmentRepository, IUnitOfWork unitOfWork)
        {
            _assessmentRepository = assessmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Assessment ID cannot be empty.");

            var assessment = await _assessmentRepository.GetByIdAsync(id);
            if (assessment == null)
                throw new NotFoundException("Assessment", id);

            await _assessmentRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}