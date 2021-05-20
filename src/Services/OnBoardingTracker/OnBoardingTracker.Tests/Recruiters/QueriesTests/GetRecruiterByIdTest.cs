using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Application.Recruiters.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Recruiters.QueriesTests
{
    public class GetRecruiterByIdTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetRecruiterById.Validator();
            validator.Validate(new GetRecruiterById { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetRecruiterById.Validator();
            validator.Validate(new GetRecruiterById { }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetRecruiterById.Handler(dbContext, AutoMapperHelper.Mapper);
            var entity = dbContext.Recruiters.First(x => x.Id == 1);
            var result = await handler.Handle(new GetRecruiterById { Id = 1 }, CancellationToken.None);
            result.ShouldBeOfType<RecruiterModel>();
            result.Id.ShouldBe(entity.Id);
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ReturnsNull()
        {
            var handler = new GetRecruiterById.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = await handler.Handle(new GetRecruiterById { Id = -1 }, CancellationToken.None);
            result.ShouldBeNull();
        }
    }
}
