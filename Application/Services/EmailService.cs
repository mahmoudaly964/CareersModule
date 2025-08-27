using Application.Services_Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _fromEmail;

        public EmailService()
        {
            _smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? "smtp-relay.brevo.com";
            _smtpPort = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out var port) ? port : 587;
            _smtpUser = Environment.GetEnvironmentVariable("SMTP_USER")??throw new ArgumentNullException("email or password is not set");
            _smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS")?? throw new ArgumentNullException("email or password is not set");
            _fromEmail = "testapp85312@gmail.com";
        }
        public async Task SendInterviewScheduledEmail(string toEmail, DateTime scheduledDate, string meetingLink)
        {
            var subject = "Interview Scheduled";
            var body = $"Your interview is scheduled for {scheduledDate:G}. Meeting link: {meetingLink}";
            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendInterviewRescheduledEmail(string toEmail, DateTime newDate, string newMeetingLink)
        {
            var subject = "Interview Rescheduled";
            var body = $"Your interview has been rescheduled to {newDate:G}. New meeting link: {newMeetingLink}";
            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendInterviewCanceledEmail(string toEmail, DateTime scheduledDate, string meetingLink)
        {
            var subject = "Interview Canceled";
            var body = $"Your interview scheduled for {scheduledDate:G} has been canceled.";
            await SendEmailAsync(toEmail, subject, body);
        }
        public async Task SendApplicationConfirmationEmail(string toEmail, string candidateName, string vacancyTitle)
        {
            var subject = "Application Submitted Successfully";
            var body = $"Dear {candidateName},\n\nYour application for '{vacancyTitle}' has been received. We will contact you soon.";
            await SendEmailAsync(toEmail, subject, body);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Careers Team", _fromEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpServer, _smtpPort, false);
            await client.AuthenticateAsync(_smtpUser, _smtpPass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}