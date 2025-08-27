using Application.DTOs.Question;
using Application.Exceptions;
using Application.UseCasesInterfaces.QuestionUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.QuestionUseCases
{
    public class UpdateQuestionUseCase : IUpdateQuestionUseCase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateQuestionUseCase(IQuestionRepository questionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(UpdateQuestionDTO updateQuestionDTO)
        {
            var existingQuestion = await _questionRepository.GetByIdAsync(updateQuestionDTO.QuestionId);
            if (existingQuestion == null)
                throw new NotFoundException("Question", updateQuestionDTO.QuestionId);

            _mapper.Map(updateQuestionDTO, existingQuestion);
            await _questionRepository.UpdateAsync(existingQuestion);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}