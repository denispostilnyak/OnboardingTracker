using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Interviews.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Interview.QueriesTests
{
    public class GetInterviewByIdTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetInterviewById.Validator();
            validator.Validate(new GetInterviewById { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetInterviewById.Validator();
            validator.Validate(new GetInterviewById { }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetInterviewById.Handler(dbContext, AutoMapperHelper.Mapper);

            var interview = await dbContext.Interviews.FirstOrDefaultAsync(interview => interview.Id == 1);

            var result = await handler.Handle(new GetInterviewById { Id = 1 }, CancellationToken.None);

            result.Id.ShouldBe(interview.Id);
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new GetInterviewById.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new GetInterviewById { Id = -1 }, CancellationToken.None);

            result.ShouldBeNull();
        }
    }
}
