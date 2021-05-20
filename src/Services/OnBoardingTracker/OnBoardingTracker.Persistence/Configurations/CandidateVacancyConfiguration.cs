using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class CandidateVacancyConfiguration : IEntityTypeConfiguration<CandidateVacancy>
    {
        public void Configure(EntityTypeBuilder<CandidateVacancy> builder)
        {
            builder.HasOne(d => d.Candidate)
                                .WithMany(p => p.CandidateVacancies)
                                .HasForeignKey(d => d.CandidateId)
                                .OnDelete(DeleteBehavior.Cascade)
                                .HasConstraintName("FK_CandidateVacancies_Candidates");

            builder.HasOne(d => d.Vacancy)
                .WithMany(p => p.CandidateVacancies)
                .HasForeignKey(d => d.VacancyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CandidateVacancies_Vacancies");
        }
    }
}
