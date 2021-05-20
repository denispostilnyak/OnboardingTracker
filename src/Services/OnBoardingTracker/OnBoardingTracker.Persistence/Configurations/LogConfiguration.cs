using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable(nameof(Log));

            builder.Property(e => e.Level)
                .HasMaxLength(128);

            builder.Property(e => e.TimeStamp)
                .IsRequired();
        }
    }
}
