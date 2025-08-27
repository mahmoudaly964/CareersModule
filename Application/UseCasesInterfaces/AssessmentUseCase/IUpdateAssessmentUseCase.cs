using Application.DTOs.Assessment;
using Application.DTOs.Question;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.AssessmentUseCase
{
    public interface IUpdateAssessmentUseCase
    {
        public Task ExecuteAsync(UpdateAssessmentDTO updateAssessmentDTO);
    }
}
