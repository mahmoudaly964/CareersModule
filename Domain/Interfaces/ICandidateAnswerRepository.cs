using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICandidateAnswerRepository : IRepository<CandidateAnswer>
    {
        Task<CandidateAnswer?> GetAnswerAsync(Guid sessionId, Guid questionId);
        Task<IEnumerable<CandidateAnswer>> GetSessionAnswersAsync(Guid sessionId);
    }
}
