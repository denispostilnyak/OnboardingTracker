using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Recruiters.Commands;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Recruiters.CommandsTests
{
    public class CreateRecruiterTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateRecruiter.Validator();
            validator.Validate(
                new CreateRecruiter
                {
                    FirstName = "NotEmpty",
                    LastName = "NotEmpty",
                    Email = "not@emp.ty"
                }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new CreateRecruiter.Validator();
            validator.Validate(
                new CreateRecruiter
                {
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Email = string.Empty
                }).Errors.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new CreateRecruiter.Handler(dbContext, AutoMapperHelper.Mapper, fileStorage);

            var entitiesBefore = await dbContext.Recruiters.ToListAsync();
            var previousCount = await dbContext.Recruiters.CountAsync();
            entitiesBefore.ShouldNotContain(c => c.FirstName == "JustCreated");

            var result = await handler.Handle(
                new CreateRecruiter
                {
                    FirstName = "JustCreated",
                    LastName = "JustCreated",
                    Email = "not@emp.ty"
                }, CancellationToken.None);

            var entitiesAfter = await dbContext.Recruiters.ToListAsync();
            entitiesAfter.Count.ShouldBe(previousCount + 1);
            entitiesAfter.ShouldContain(c => c.FirstName == "JustCreated");
            result.ShouldBeOfType<RecruiterModel>();
            result.FirstName.ShouldBe("JustCreated");
        }
    }
}
