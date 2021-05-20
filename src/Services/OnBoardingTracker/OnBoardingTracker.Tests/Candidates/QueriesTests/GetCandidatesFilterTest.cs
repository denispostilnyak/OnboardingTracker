using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Candidates.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Candidates.QueriesTests
{
    public class GetCandidatesFilterTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetCandidatesFilter.Validator();
            validator.Validate(new GetCandidatesFilter { Page = 1, Limit = 10 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetCandidatesFilter.Validator();
            validator.Validate(new GetCandidatesFilter { }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetCandidatesFilter.Handler(AutoMapperHelper.Mapper, dbContext);

            var candidates = await dbContext.Candidates.Where(x => x.OriginId == 1).Take(5).ToListAsync();

            var result = await handler.Handle(new GetCandidatesFilter { Page = 1, Limit = 5, OriginId = 1 }, CancellationToken.None);

            result.Count.ShouldBe(candidates.Count);
        }
    }
}
