using Application.DTOs.Assessment;
using Application.Exceptions;
using Application.UseCasesInterfaces.AssessmentUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.AssessmentUseCases
{
    public class CreateAssessmentUseCase : ICreateAssessmentUseCase
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAssessmentUseCase(
            IAssessmentRepository assessmentRepository,
            IVacancyRepository vacancyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _assessmentRepository = assessmentRepository;
            _vacancyRepository = vacancyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> ExecuteAsync(CreateAssessmentDTO createAssessmentDTO)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(createAssessmentDTO.VacancyId);
            if (vacancy == null)
                throw new NotFoundException("Vacancy", createAssessmentDTO.VacancyId);

            if (createAssessmentDTO.Deadline <= DateTime.UtcNow)
                throw new InvalidOperationException("Deadline must be in the future");

            if (!createAssessmentDTO.Questions.Any())
                throw new InvalidOperationException("Assessment must have at least one question");

            foreach (var question in createAssessmentDTO.Questions.Where(q => q.Type == QuestionType.MultipleChoice))
            {
                if (question.Options == null || !question.Options.Any())
                    throw new InvalidOperationException("Multiple choice questions must have options");

                if (!question.Options.Any(o => o.IsCorrect))
                    throw new InvalidOperationException("Multiple choice questions must have at least one correct option");
            }

            var assessment = _mapper.Map<Assessment>(createAssessmentDTO);
            assessment.IsActive = true;
            assessment.CreatedAt = DateTime.UtcNow;

            assessment.Questions = createAssessmentDTO.Questions.Select(questionDto =>
            {
                var question = _mapper.Map<Question>(questionDto);
                question.Assessment = assessment;
                question.CreatedAt = DateTime.UtcNow;

                if (questionDto.Type == QuestionType.MultipleChoice && questionDto.Options != null)
                {
                    question.Options = questionDto.Options.Select(optionDto =>
                    {
                        var option = _mapper.Map<QuestionOption>(optionDto);
                        option.CreatedAt = DateTime.UtcNow;
                        return option;
                    }).ToList();
                }

                return question;
            }).ToList();

            await _assessmentRepository.AddAsync(assessment);
            await _unitOfWork.SaveChangesAsync();

            return assessment.Id;
        }
    }
}