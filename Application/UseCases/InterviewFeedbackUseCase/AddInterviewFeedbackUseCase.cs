using Application.DTOs.InterviewFeedback;
using Application.UseCasesInterfaces.InterviewFeedback;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.InterviewFeedbackUseCase
{
    public class AddInterviewFeedbackUseCase : IAddInterviewFeedbackUseCase
    {
        private readonly IInterviewFeedbackRepository _interviewFeedbackRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddInterviewFeedbackUseCase(
            IInterviewFeedbackRepository interviewFeedbackRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _interviewFeedbackRepository = interviewFeedbackRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> ExecuteAsync(AddInterviewFeedbackDTO dto)
        {
            var feedback = _mapper.Map<InterviewFeedback>(dto);
            feedback.CreatedAt = DateTime.UtcNow;

            await _interviewFeedbackRepository.AddAsync(feedback);
            await _unitOfWork.SaveChangesAsync();

            return feedback.Id;
        }
    }
}