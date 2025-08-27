using Application.DTOs.Question;

namespace Application.Services_Interfaces
{
    public interface IQuestionService
    {
        Task<Guid> AddQuestionAsync(AddQuestionDTO addQuestionDTO);
        Task UpdateQuestionAsync(UpdateQuestionDTO updateQuestionDTO);
        Task DeleteQuestionAsync(Guid id);
    }
}