using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class InterviewConfiguration : EntityConfigurationBase<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {
            builder.Property(e => e.EndingTime).HasColumnType("datetime");

            builder.Property(e => e.StartingTime).HasColumnType("datetime");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(d => d.Candidate)
                .WithMany(p => p.Interviews)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Interviews_Candidates");

            builder.HasOne(d => d.Vacancy)
                .WithMany(p => p.Interviews)
                .HasForeignKey(d => d.VacancyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Interviews_Vacancies");

            builder.HasOne(d => d.Recruiter)
                .WithMany(p => p.Interviews)
                .HasForeignKey(d => d.RecruiterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Interviews_Recruiters");

            base.Configure(builder);
        }
    }
}
