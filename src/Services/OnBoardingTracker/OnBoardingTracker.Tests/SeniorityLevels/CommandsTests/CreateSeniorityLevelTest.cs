using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.SeniorityLevels.Commands;
using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.SeniorityLevels.CommandsTests
{
    public class CreateSeniorityLevelTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateSeniorityLevel.Validator();
            validator.Validate(new CreateSeniorityLevel { Name = "NotEmpty" }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new CreateSeniorityLevel.Validator();
            validator.Validate(new CreateSeniorityLevel { Name = string.Empty }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new CreateSeniorityLevel.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.SeniorityLevels.ToListAsync();
            var previousCount = entitiesBefore.Count;
            entitiesBefore.ShouldNotContain(c => c.Name.Equals("JustCreated"));

            var result = await handler.Handle(new CreateSeniorityLevel { Name = "JustCreated" }, CancellationToken.None);

            var entitiesAfter = await dbContext.SeniorityLevels.ToListAsync();
            entitiesAfter.Count.ShouldBe(previousCount + 1);
            entitiesAfter.ShouldContain(c => c.Name.Equals("JustCreated"));
            result.ShouldBeOfType<SeniorityLevelModel>();
        }
    }
}
