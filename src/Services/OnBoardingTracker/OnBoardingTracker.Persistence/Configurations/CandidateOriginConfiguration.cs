using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class CandidateOriginConfiguration : EntityConfigurationBase<CandidateOrigin>
    {
        public void Configure(EntityTypeBuilder<CandidateOrigin> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            base.Configure(builder);
        }
    }
}
