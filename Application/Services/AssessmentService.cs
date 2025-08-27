using Application.DTOs.Assessment;
using Application.Services_Interfaces;
using Application.UseCasesInterfaces.AssessmentUseCase;

namespace Application.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly ICreateAssessmentUseCase _createAssessmentUseCase;
        private readonly IGetAssessmentUseCase _getAssessmentUseCase;
        private readonly IStartAssessmentUseCase _startAssessmentUseCase;
        private readonly ISubmitAnswerUseCase _submitAnswerUseCase;
        private readonly ISubmitAssessmentUseCase _submitAssessmentUseCase;
        private readonly IStartQuestionUseCase _startQuestionUseCase;

        public AssessmentService(
            ICreateAssessmentUseCase createAssessmentUseCase,
            IGetAssessmentUseCase getAssessmentUseCase,
            IStartAssessmentUseCase startAssessmentUseCase,
            ISubmitAnswerUseCase submitAnswerUseCase,
            ISubmitAssessmentUseCase submitAssessmentUseCase,
            IStartQuestionUseCase startQuestionUseCase)
        {
            _createAssessmentUseCase = createAssessmentUseCase;
            _getAssessmentUseCase = getAssessmentUseCase;
            _startAssessmentUseCase = startAssessmentUseCase;
            _submitAnswerUseCase = submitAnswerUseCase;
            _submitAssessmentUseCase = submitAssessmentUseCase;
            _startQuestionUseCase = startQuestionUseCase;
        }

        public async Task<Guid> CreateAssessmentAsync(CreateAssessmentDTO createAssessmentDTO)
        {
            return await _createAssessmentUseCase.ExecuteAsync(createAssessmentDTO);
        }
        public async Task<AssessmentResponseDTO> GetAssessmentAsync(Guid assessmentId)
        {
            return await _getAssessmentUseCase.ExecuteAsync(assessmentId);
        }
        public async Task<AssessmentSessionResponseDTO> StartAssessmentAsync(StartAssessmentDTO startAssessmentDTO)
        {
            return await _startAssessmentUseCase.ExecuteAsync(startAssessmentDTO);
        }

        public async Task<QuestionResponseDTO> StartQuestionAsync(StartQuestionDTO startQuestionDTO)
        {
            return await _startQuestionUseCase.ExecuteAsync(startQuestionDTO);
        }

        public async Task SubmitAnswerAsync(SubmitAnswerDTO submitAnswerDTO)
        {
            await _submitAnswerUseCase.ExecuteAsync(submitAnswerDTO);
        }

        public async Task SubmitAssessmentAsync(SubmitAssessmentDTO submitAssessmentDTO)
        {
            await _submitAssessmentUseCase.ExecuteAsync(submitAssessmentDTO);
        }
    }
}