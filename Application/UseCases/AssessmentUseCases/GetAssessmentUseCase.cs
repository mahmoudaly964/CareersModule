using Application.DTOs.Assessment;
using Application.Exceptions;
using Application.UseCasesInterfaces.AssessmentUseCase;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.AssessmentUseCases
{
    public class GetAssessmentUseCase : IGetAssessmentUseCase
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IMapper _mapper;

        public GetAssessmentUseCase(IAssessmentRepository assessmentRepository, IMapper mapper)
        {
            _assessmentRepository = assessmentRepository;
            _mapper = mapper;
        }

        public async Task<AssessmentResponseDTO> ExecuteAsync(Guid assessmentId)
        {
            var assessment = await _assessmentRepository.GetAssessmentAsync(assessmentId);
            if (assessment == null)
            {
                throw new NotFoundException("Assessment", assessmentId);
            }

            return _mapper.Map<AssessmentResponseDTO>(assessment);
        }
    }
}