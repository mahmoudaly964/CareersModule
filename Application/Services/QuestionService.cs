using Application.DTOs.Question;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.QuestionUseCase;

namespace Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IAddQuestionUseCase _addQuestionUseCase;
        private readonly IUpdateQuestionUseCase _updateQuestionUseCase;
        private readonly IDeleteQuestionUseCase _deleteQuestionUseCase;

        public QuestionService(
            IAddQuestionUseCase addQuestionUseCase,
            IUpdateQuestionUseCase updateQuestionUseCase,
            IDeleteQuestionUseCase deleteQuestionUseCase)
        {
            _addQuestionUseCase = addQuestionUseCase;
            _updateQuestionUseCase = updateQuestionUseCase;
            _deleteQuestionUseCase = deleteQuestionUseCase;
        }

        public async Task<Guid> AddQuestionAsync(AddQuestionDTO addQuestionDTO)
        {
            return await _addQuestionUseCase.ExecuteAsync(addQuestionDTO);
        }
        public async Task UpdateQuestionAsync(UpdateQuestionDTO updateQuestionDTO)
        {
            await _updateQuestionUseCase.ExecuteAsync(updateQuestionDTO);
        }
        public async Task DeleteQuestionAsync(Guid id)
        {
            await _deleteQuestionUseCase.ExecuteAsync(id);
        }
    }
}