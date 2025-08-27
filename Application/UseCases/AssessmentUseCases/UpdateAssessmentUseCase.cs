using Application.DTOs.Assessment;
using Application.Exceptions;
using Application.UseCasesInterfaces.AssessmentUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AssessmentUseCases
{
    public class UpdateAssessmentUseCase : IUpdateAssessmentUseCase
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAssessmentUseCase(IAssessmentRepository assessmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _assessmentRepository = assessmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(UpdateAssessmentDTO updateAssessmentDTO)
        {
            var existingAssessment = await _assessmentRepository.GetByIdAsync(updateAssessmentDTO.AssessmentId);
            if (existingAssessment == null)
                throw new NotFoundException("Assessment", updateAssessmentDTO.AssessmentId);

            _mapper.Map(updateAssessmentDTO, existingAssessment);
            await _assessmentRepository.UpdateAsync(existingAssessment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}