using System.Threading.Tasks;

namespace Application.Services_Interfaces
{
    public interface IEmailService
    {
        Task SendInterviewScheduledEmail(string toEmail, DateTime scheduledDate, string meetingLink);
        Task SendInterviewRescheduledEmail(string toEmail, DateTime newDate, string newMeetingLink);
        Task SendInterviewCanceledEmail(string toEmail, DateTime scheduledDate, string meetingLink);
        Task SendApplicationConfirmationEmail(string toEmail, string candidateName, string vacancyTitle);
    }
}