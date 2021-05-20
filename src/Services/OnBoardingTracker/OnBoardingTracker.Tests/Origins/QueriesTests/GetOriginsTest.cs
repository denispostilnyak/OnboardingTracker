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
    public class GetOriginsTest : BaseTest
    {
        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetOrigins.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = await handler.Handle(new GetOrigins(), CancellationToken.None);
            result.ShouldBeOfType<OriginList>();

            result.Items.Count().ShouldBe(dbContext.CandidateOrigins.Count());
        }
    }
}
