using OnBoardingTracker.Application.JobTypes.Models;
using OnBoardingTracker.Application.JobTypes.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.JobTypes.QueriesTests
{
    public class GetJobTypesTest : BaseTest
    {
        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetJobTypes.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = await handler.Handle(new GetJobTypes(), CancellationToken.None);
            result.ShouldBeOfType<JobTypeList>();

            result.Items.Count().ShouldBe(dbContext.JobTypes.Count());
        }
    }
}
