using Application.DTOs.Question;
using Application.Exceptions;
using Application.UseCasesInterfaces.QuestionUseCase;
using Domain.Interfaces;

namespace Application.UseCases.QuestionUseCases
{
    public class DeleteQuestionUseCase : IDeleteQuestionUseCase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionUseCase(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
        {
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Question ID cannot be empty.");

            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
                throw new NotFoundException("Question", id);

            await _questionRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}