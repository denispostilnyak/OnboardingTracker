using OnBoardingTracker.Application.Origins.Queries;
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
    public class GetRecruitersTest : BaseTest
    {
        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetRecruiters.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = await handler.Handle(new GetRecruiters(), CancellationToken.None);
            result.ShouldBeOfType<RecruiterList>();

            result.Items.Count().ShouldBe(dbContext.Recruiters.Count());
        }
    }
}
