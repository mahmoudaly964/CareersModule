using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCasesInterfaces.AssessmentUseCase
{
    public interface IDeleteAssessmentUseCase
    {
        public Task ExecuteAsync(Guid id);
    }
}
