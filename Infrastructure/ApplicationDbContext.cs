using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentSession> AssessmentSessions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateAnswer> CandidateAnswers { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<InterviewFeedback> InterviewFeedbacks { get; set; }
        public DbSet<QuestionSession> QuestionSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureOneToOneRelationships(modelBuilder);

            ConfigureOneToManyRelationships(modelBuilder);

            ConfigureConstraints(modelBuilder);
        }

        private void ConfigureOneToOneRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Candidate)
                .WithOne(c => c.User)
                .HasForeignKey<Candidate>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureOneToManyRelationships(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Applications)
                .WithOne(a => a.Candidate)
                .HasForeignKey(a => a.CandidateId);

            modelBuilder.Entity<Vacancy>()
                .HasMany(v => v.Applications)
                .WithOne(a => a.Vacancy)
                .HasForeignKey(a => a.VacancyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vacancy>()
                .HasMany(v => v.Assessments)
                .WithOne(a => a.Vacancy)
                .HasForeignKey(a => a.VacancyId);

            modelBuilder.Entity<Assessment>()
                .HasMany(a => a.Questions)
                .WithOne(q => q.Assessment)
                .HasForeignKey(q => q.AssessmentId);

            modelBuilder.Entity<Assessment>()
                .HasMany(a => a.AssessmentSessions)
                .WithOne(s => s.Assessment)
                .HasForeignKey(s => s.AssessmentId);

            modelBuilder.Entity<Application>()
                .HasMany(a => a.AssessmentSessions)
                .WithOne(s => s.Application)
                .HasForeignKey(s => s.ApplicationId);

            modelBuilder.Entity<Application>()
                .HasMany(a => a.Interviews)
                .WithOne(i => i.Application)
                .HasForeignKey(i => i.ApplicationId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId);
            modelBuilder.Entity<Question>()
                .HasMany(q => q.CandidateAnswers)
                .WithOne(ca => ca.Question)
                .HasForeignKey(ca => ca.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionOption>()
                .HasMany(o => o.CandidateAnswers)
                .WithOne(ca => ca.SelectedOption)
                .HasForeignKey(ca => ca.SelectedOptionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AssessmentSession>()
                .HasMany(s => s.Answers)
                .WithOne(ca => ca.AssessmentSession)
                .HasForeignKey(ca => ca.AssessmentSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Interview>()
                .HasMany(i => i.Feedbacks)
                .WithOne(f => f.Interview)
                .HasForeignKey(f => f.InterviewId);

            modelBuilder.Entity<AssessmentSession>()
                .HasMany(s => s.QuestionSessions)
                .WithOne(qs => qs.AssessmentSession)
                .HasForeignKey(qs => qs.AssessmentSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.QuestionSessions)
                .WithOne(qs => qs.Question)
                .HasForeignKey(qs => qs.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateAnswer>()
                .HasIndex(ca => new { ca.AssessmentSessionId, ca.QuestionId })
                .IsUnique();

            modelBuilder.Entity<Application>()
                .HasIndex(a => new { a.CandidateId, a.VacancyId })
                .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}