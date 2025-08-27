using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAssessmentSessionRepository : IRepository<AssessmentSession>
    {
        Task<AssessmentSession?> GetActiveSessionAsync(Guid applicationId, Guid assessmentId);
        Task<AssessmentSession?> GetSessionWithAnswersAsync(Guid sessionId);
        //Task<IEnumerable<AssessmentSession>> GetSessionsByApplicationIdAsync(Guid applicationId);
    }
}
