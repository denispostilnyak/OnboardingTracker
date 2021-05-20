using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Application.SeniorityLevels.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.SeniorityLevels.QueriesTests
{
    public class GetSeniorityLevelByIdTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetSeniorityLevelById.Validator();
            validator.Validate(new GetSeniorityLevelById { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetSeniorityLevelById.Validator();
            validator.Validate(new GetSeniorityLevelById()).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetSeniorityLevelById.Handler(dbContext, AutoMapperHelper.Mapper);
            var entity = dbContext.SeniorityLevels.First(x => x.Id == 1);
            var result = await handler.Handle(new GetSeniorityLevelById { Id = 1 }, CancellationToken.None);
            result.Name.ShouldBe(entity.Name);
            result.ShouldBeOfType<SeniorityLevelModel>();
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ReturnsNull()
        {
            var handler = new GetSeniorityLevelById.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = await handler.Handle(new GetSeniorityLevelById { Id = -1 }, CancellationToken.None);
            result.ShouldBeNull();
        }
    }
}
