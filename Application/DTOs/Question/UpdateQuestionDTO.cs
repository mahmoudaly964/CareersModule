using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.DTOs.Question
{
    public class UpdateQuestionDTO
    {
        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        [StringLength(1000)]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        public QuestionType Type { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Time limit must be positive")]
        public int TimeLimit { get; set; }

        public List<UpdateQuestionOptionDTO>? Options { get; set; }
    }

    public class UpdateQuestionOptionDTO
    {
        [Required]
        public Guid OptionId { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}