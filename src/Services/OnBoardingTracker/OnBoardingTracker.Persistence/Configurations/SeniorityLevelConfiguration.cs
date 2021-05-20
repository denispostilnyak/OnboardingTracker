using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class SeniorityLevelConfiguration : EntityConfigurationBase<SeniorityLevel>
    {
        public void Configure(EntityTypeBuilder<SeniorityLevel> builder)
        {
            builder.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(50);

            base.Configure(builder);
        }
    }
}
