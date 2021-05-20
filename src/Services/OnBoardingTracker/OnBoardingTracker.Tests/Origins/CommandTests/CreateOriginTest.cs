using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Origins.Commands;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Origins.CommandTests
{
    public class CreateOriginTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateOrigin.Validator();
            validator.Validate(new CreateOrigin { Name = "NotEmpty" }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new CreateOrigin.Validator();
            validator.Validate(new CreateOrigin { Name = string.Empty }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new CreateOrigin.Handler(dbContext, AutoMapperHelper.Mapper);

            var entitiesBefore = await dbContext.CandidateOrigins.ToListAsync();
            var previousCount = entitiesBefore.Count;
            entitiesBefore.ShouldNotContain(c => c.Name.Equals("JustCreated"));

            var result = await handler.Handle(new CreateOrigin { Name = "JustCreated" }, CancellationToken.None);

            var entitiesAfter = await dbContext.CandidateOrigins.ToListAsync();
            entitiesAfter.Count.ShouldBe(previousCount + 1);
            entitiesAfter.ShouldContain(c => c.Name.Equals("JustCreated"));
            result.ShouldBeOfType<OriginModel>();
        }
    }
}
