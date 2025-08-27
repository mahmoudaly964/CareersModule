using Application.DTOs.Assessment;
using Application.Exceptions;
using Application.UseCasesInterfaces.AssessmentUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AssessmentUseCases
{
    public class StartQuestionUseCase : IStartQuestionUseCase
    {
        private readonly IQuestionSessionRepository _questionSessionRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StartQuestionUseCase(
            IQuestionSessionRepository questionSessionRepository,
            IQuestionRepository questionRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _questionSessionRepository = questionSessionRepository;
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<QuestionResponseDTO> ExecuteAsync(StartQuestionDTO startQuestionDTO)
        {
            var existing = await _questionSessionRepository.GetQuestionSessionAsync(
                startQuestionDTO.AssessmentSessionId, startQuestionDTO.QuestionId);

            // Fetch the question
            var question = await _questionRepository.GetByIdAsync(startQuestionDTO.QuestionId);
            if (question == null)
                throw new NotFoundException("Question", startQuestionDTO.QuestionId);

            if (existing == null)
            {
                var questionSession = _mapper.Map<QuestionSession>(startQuestionDTO);
                questionSession.CreatedAt = DateTime.UtcNow;
                questionSession.StartTime = DateTime.UtcNow;

                await _questionSessionRepository.AddAsync(questionSession);
                await _unitOfWork.SaveChangesAsync();
            }

            return _mapper.Map<QuestionResponseDTO>(question);
        }
    }
}