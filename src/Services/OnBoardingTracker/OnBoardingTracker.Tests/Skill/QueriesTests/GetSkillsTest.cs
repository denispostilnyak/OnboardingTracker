using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Skills.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Skill.QueriesTests
{
    public class GetSkillsTest : BaseTest
    {
        [Fact]
        public async Task Handle_Posititve()
        {
            var handler = new GetSkills.Handler(dbContext, AutoMapperHelper.Mapper);

            var skillsList = await dbContext.Skills.ToListAsync();

            var result = await handler.Handle(new GetSkills { }, CancellationToken.None);

            result.Items.Count().ShouldBe(skillsList.Count);
        }
    }
}
