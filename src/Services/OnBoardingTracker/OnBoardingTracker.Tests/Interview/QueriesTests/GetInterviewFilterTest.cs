using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Candidates.Queries;
using OnBoardingTracker.Application.Interviews.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Interview.QueriesTests
{
    public class GetInterviewFilterTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetInterviewsFilter.Validator();
            validator.Validate(new GetInterviewsFilter { Page = 1, Limit = 10 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetInterviewsFilter.Validator();
            validator.Validate(new GetInterviewsFilter { }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetInterviewsFilter.Handler(AutoMapperHelper.Mapper, dbContext);

            var interview = await dbContext.Interviews.Where(interview => interview.CandidateId == 1).Take(5).ToListAsync();

            var result = await handler.Handle(new GetInterviewsFilter { Page = 1, Limit = 5, CandidateId = 1 }, CancellationToken.None);

            result.Count.ShouldBe(interview.Count);
        }
    }
}
