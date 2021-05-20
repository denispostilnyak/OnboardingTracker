using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class RecruiterConfiguration : EntityConfigurationBase<Recruiter>
    {
        public override void Configure(EntityTypeBuilder<Recruiter> builder)
        {
            builder.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            base.Configure(builder);
        }
    }
}
