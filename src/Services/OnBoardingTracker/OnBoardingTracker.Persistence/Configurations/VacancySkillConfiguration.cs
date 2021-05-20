using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class VacancySkillConfiguration : IEntityTypeConfiguration<VacancySkill>
    {
        public void Configure(EntityTypeBuilder<VacancySkill> builder)
        {
            builder.HasOne(d => d.Skill)
                    .WithMany(p => p.VacancySkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_VacancySkills_Skills");

            builder.HasOne(d => d.Vacancy)
                .WithMany(p => p.VacancySkills)
                .HasForeignKey(d => d.VacancyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VacancySkills_Vacancies");
        }
    }
}
