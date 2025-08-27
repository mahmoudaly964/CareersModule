using Application.DTOs.Question;
using Application.UseCasesInterfaces.QuestionUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.QuestionUseCases
{
    public class AddQuestionUseCase : IAddQuestionUseCase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddQuestionUseCase(IQuestionRepository questionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> ExecuteAsync(AddQuestionDTO newQuestion)
        {
            if (newQuestion == null)
                throw new ArgumentNullException(nameof(newQuestion), "Question data cannot be null.");

            var question = _mapper.Map<Question>(newQuestion);
            question.CreatedAt = DateTime.UtcNow;
            await _questionRepository.AddAsync(question);
            await _unitOfWork.SaveChangesAsync();
            return question.Id;
        }
    }
}