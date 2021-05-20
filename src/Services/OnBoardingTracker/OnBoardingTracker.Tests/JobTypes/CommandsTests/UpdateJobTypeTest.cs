using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.JobTypes.Commands;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.JobTypes.CommandsTests
{
    public class UpdateJobTypeTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new UpdateJobType.Validator();
            validator.Validate(new UpdateJobType { Id = 1, Name = "NotEmpty" }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new UpdateJobType.Validator();
            validator.Validate(new UpdateJobType { }).Errors.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new UpdateJobType.Handler(dbContext, AutoMapperHelper.Mapper);
            var entity = dbContext.JobTypes.First(x => x.Id == 1);

            var result = await handler.Handle(new UpdateJobType { Id = 1, Name = "Updated" }, CancellationToken.None);
            result.Name.ShouldBe("Updated");
            entity.Name.ShouldBe("Updated");
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new UpdateJobType.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = handler.Handle(new UpdateJobType { Id = -1, Name = "Updated" }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
