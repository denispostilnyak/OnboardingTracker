using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.JobTypes.Commands;
using OnBoardingTracker.Application.JobTypes.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.JobTypes.CommandsTests
{
    public class CreateJobTypeTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateJobType.Validator();
            validator.Validate(new CreateJobType { Name = "NotEmpty" }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new CreateJobType.Validator();
            validator.Validate(new CreateJobType { Name = string.Empty }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new CreateJobType.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.JobTypes.ToListAsync();
            var previousCount = entitiesBefore.Count;
            entitiesBefore.ShouldNotContain(c => c.Name.Equals("JustCreated"));

            var result = await handler.Handle(new CreateJobType { Name = "JustCreated" }, CancellationToken.None);

            var entitiesAfter = await dbContext.JobTypes.ToListAsync();
            entitiesAfter.Count.ShouldBe(previousCount + 1);
            entitiesAfter.ShouldContain(c => c.Name.Equals("JustCreated"));
            result.ShouldBeOfType<JobTypeModel>();
        }
    }
}
