using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs.Question
{
    public class AddQuestionDTO
    {
        [Required]
        public Guid AssessmentId { get; set; }

        [Required]
        [StringLength(1000)]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        public QuestionType Type { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Time limit must be positive")]
        public int TimeLimit { get; set; }

        public List<AddQuestionOptionDTO>? Options { get; set; }
    }

    public class AddQuestionOptionDTO
    {
        [Required]
        [StringLength(500)]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}