using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Recruiters.Commands;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Recruiters.CommandsTests
{
    public class UpdateRecruiterTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new UpdateRecruiter.Validator();
            validator.Validate(
                new UpdateRecruiter
                {
                    Id = 1,
                    FirstName = "NotEmpty",
                    LastName = "NotEmpty",
                    Email = "not@emp.ty"
                }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new UpdateRecruiter.Validator();
            validator.Validate(new UpdateRecruiter()).Errors.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new UpdateRecruiter.Handler(dbContext, AutoMapperHelper.Mapper, fileStorage);
            var entity = dbContext.Recruiters.First(x => x.Id == 1);
            var result = await handler.Handle(
                new UpdateRecruiter
                {
                    Id = 1,
                    FirstName = "Updated",
                    LastName = "Updated",
                    Email = "upd@at.ed"
                }, CancellationToken.None);

            result.ShouldBeOfType<RecruiterModel>();
            result.FirstName.ShouldBe("Updated");
            entity.FirstName.ShouldBe("Updated");
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new UpdateRecruiter.Handler(dbContext, AutoMapperHelper.Mapper, fileStorage);
            var result = handler.Handle(new UpdateRecruiter { Id = -1 }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
