using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.SeniorityLevels.Commands;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.SeniorityLevels.CommandsTests
{
    public class UpdateSeniorityLevelTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new UpdateSeniorityLevel.Validator();
            validator.Validate(new UpdateSeniorityLevel { Id = 1, Name = "NotEmpty" }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new UpdateSeniorityLevel.Validator();
            validator.Validate(new UpdateSeniorityLevel { }).Errors.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new UpdateSeniorityLevel.Handler(dbContext, AutoMapperHelper.Mapper);
            var entity = dbContext.SeniorityLevels.First(x => x.Id == 1);

            var result = await handler.Handle(new UpdateSeniorityLevel { Id = 1, Name = "Updated" }, CancellationToken.None);
            result.Name.ShouldBe("Updated");
            entity.Name.ShouldBe("Updated");
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new UpdateSeniorityLevel.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = handler.Handle(new UpdateSeniorityLevel { Id = -1, Name = "Updated" }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
