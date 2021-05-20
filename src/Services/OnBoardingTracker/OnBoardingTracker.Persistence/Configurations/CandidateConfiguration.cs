using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class CandidateConfiguration : EntityConfigurationBase<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.Property(e => e.CurrentJobInformation)
                    .HasMaxLength(250);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Origin)
                .WithMany(p => p.Candidates)
                .HasForeignKey(d => d.OriginId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Candidates_CandidateOrigins");

            builder.Property(x => x.CvUrl)
                .HasMaxLength(500);
            base.Configure(builder);
        }
    }
}
