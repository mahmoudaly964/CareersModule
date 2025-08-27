using Application.DTOs.Assessment;
using Application.Exceptions;
using Application.UseCasesInterfaces.AssessmentUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Application.UseCases.AssessmentUseCases
{
    public class SubmitAnswerUseCase : ISubmitAnswerUseCase
    {
        private readonly IAssessmentSessionRepository _assessmentSessionRepository;
        private readonly ICandidateAnswerRepository _candidateAnswerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuestionSessionRepository _questionSessionRepository;

        public SubmitAnswerUseCase(
            IAssessmentSessionRepository assessmentSessionRepository,
            ICandidateAnswerRepository candidateAnswerRepository,
            IUnitOfWork unitOfWork,IMapper mapper,
            IQuestionSessionRepository questionSessionRepository
            )
        {
            _assessmentSessionRepository = assessmentSessionRepository;
            _candidateAnswerRepository = candidateAnswerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _questionSessionRepository = questionSessionRepository;
        }

        public async Task ExecuteAsync(SubmitAnswerDTO submitAnswerDTO)
        {
            var session = await _assessmentSessionRepository.GetSessionWithAnswersAsync(submitAnswerDTO.AssessmentSessionId);
            if (session == null)
            {
                throw new NotFoundException("Assessment session", submitAnswerDTO.AssessmentSessionId);
            }

            if (session.IsSubmitted)
            {
                throw new InvalidOperationException("Assessment has already been submitted");
            }

            var expectedEndTime = session.StartTime.AddSeconds(session.Assessment.TotalDuration);
            if (DateTime.UtcNow > expectedEndTime)
            {
                session.IsSubmitted = true;
                session.EndTime = expectedEndTime;
                throw new InvalidOperationException("Assessment time has expired");
            }

            var question = session.Assessment.Questions.FirstOrDefault(q => q.Id == submitAnswerDTO.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found in this assessment");
            }
            var questionSession = await _questionSessionRepository.GetQuestionSessionAsync(session.Id, question.Id);
            if (questionSession == null)
                throw new InvalidOperationException("Question session not started");

            var timeSpent = DateTime.UtcNow - questionSession.StartTime;
            if (timeSpent.TotalSeconds > question.TimeLimit)
                throw new InvalidOperationException("Time limit for this question has expired.");


            var existingAnswer = await _candidateAnswerRepository.GetAnswerAsync(
                submitAnswerDTO.AssessmentSessionId, submitAnswerDTO.QuestionId);

            if (existingAnswer != null)
            {
                existingAnswer.AnswerText = submitAnswerDTO.AnswerText;
                existingAnswer.SelectedOptionId = submitAnswerDTO.SelectedOptionId;
                await _candidateAnswerRepository.UpdateAsync(existingAnswer);
            }
            else
            {
                var answer = _mapper.Map<CandidateAnswer>(submitAnswerDTO);
                answer.CreatedAt = DateTime.UtcNow;
                await _candidateAnswerRepository.AddAsync(answer);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}