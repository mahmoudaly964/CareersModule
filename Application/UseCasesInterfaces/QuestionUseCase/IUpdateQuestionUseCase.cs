using Application.DTOs.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.QuestionUseCase
{
    public interface IUpdateQuestionUseCase
    {
        public Task ExecuteAsync(UpdateQuestionDTO updateQuestionDTO);
    }
}
