using Application.DTOs.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.QuestionUseCase
{
    public interface IAddQuestionUseCase
    {
        public Task<Guid> ExecuteAsync(AddQuestionDTO newQuestion);
    }
}
