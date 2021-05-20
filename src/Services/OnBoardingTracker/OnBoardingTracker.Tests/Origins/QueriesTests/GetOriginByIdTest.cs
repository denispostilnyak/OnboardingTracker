using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Application.Origins.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Origins.QueriesTests
{
    public class GetOriginByIdTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetOriginById.Validator();
            validator.Validate(new GetOriginById { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetOriginById.Validator();
            validator.Validate(new GetOriginById { }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetOriginById.Handler(dbContext, AutoMapperHelper.Mapper);
            var entity = dbContext.CandidateOrigins.First(x => x.Id == 1);
            var result = await handler.Handle(new GetOriginById { Id = 1 }, CancellationToken.None);
            result.ShouldBeOfType<OriginModel>();
            result.Id.ShouldBe(entity.Id);
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ReturnsNull()
        {
            var handler = new GetOriginById.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = await handler.Handle(new GetOriginById { Id = -1 }, CancellationToken.None);
            result.ShouldBeNull();
        }
    }
}
