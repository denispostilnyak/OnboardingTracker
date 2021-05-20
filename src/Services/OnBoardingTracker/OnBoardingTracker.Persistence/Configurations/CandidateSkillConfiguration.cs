using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class CandidateSkillConfiguration : IEntityTypeConfiguration<CandidateSkill>
    {
        public void Configure(EntityTypeBuilder<CandidateSkill> builder)
        {
            builder.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateSkills)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CandidateSkills_Candidates");

            builder.HasOne(d => d.Skill)
                .WithMany(p => p.CandidateSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CandidateSkills_Skills");
        }
    }
}
