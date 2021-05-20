using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class VacancyConfiguration : EntityConfigurationBase<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.Property(e => e.MaxSalary).HasColumnType("money");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
               .IsRequired()
               .HasMaxLength(1000);

            builder.HasOne(d => d.AssignedRecruiter)
                .WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.AssignedRecruiterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vacancies_Recruiters");

            builder.HasOne(d => d.JobType)
                .WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vacancies_JobTypes");

            builder.HasOne(d => d.SeniorityLevel)
                .WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.SeniorityLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vacancies_SeniorityLevels");

            builder.HasOne(d => d.VacancyStatus)
                .WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.VacancyStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vacancies_VacancyStatuses");
            base.Configure(builder);
        }
    }
}
