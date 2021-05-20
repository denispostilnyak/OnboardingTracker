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
    public class GetCandidatesTest : BaseTest
    {
        [Fact]
        public async Task Handle_Positive()
        {
            var candidates = await dbContext.Candidates.ToListAsync();
            var result = await new GetCandidates.Handler(dbContext, AutoMapperHelper.Mapper)
                .Handle(new GetCandidates(), CancellationToken.None);
            result.Items.Count().ShouldBe(candidates.Count);
        }
    }
}
