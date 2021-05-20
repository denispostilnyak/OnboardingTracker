using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Application.SeniorityLevels.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.SeniorityLevels.QueriesTests
{
    public class GetSeniorityLevelsTest : BaseTest
    {
        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetSeniorityLevels.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = await handler.Handle(new GetSeniorityLevels(), CancellationToken.None);
            result.ShouldBeOfType<SeniorityLevelList>();

            result.Items.Count().ShouldBe(dbContext.SeniorityLevels.Count());
        }
    }
}
