using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class SkillConfiguration : EntityConfigurationBase<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

            base.Configure(builder);
        }
    }
}
