using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Interviews.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Interview.QueriesTests
{
    public class GetInterviewsTest : BaseTest
    {
        [Fact]
        public async Task Handle_Posititve()
        {
            var handler = new GetInterviews.Handler(dbContext, AutoMapperHelper.Mapper);

            var interviewList = await dbContext.Interviews.ToListAsync();

            var result = await handler.Handle(new GetInterviews { }, CancellationToken.None);

            result.Items.Count().ShouldBe(interviewList.Count);
        }
    }
}
