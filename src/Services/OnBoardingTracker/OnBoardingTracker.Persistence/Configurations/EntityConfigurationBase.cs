using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Persistence.Configurations
{
    public class EntityConfigurationBase<T> : IEntityTypeConfiguration<T>
        where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasQueryFilter(t => !t.IsDeleted);
        }
    }
}
