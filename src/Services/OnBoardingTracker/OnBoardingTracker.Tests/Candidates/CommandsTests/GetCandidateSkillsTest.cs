using OnBoardingTracker.Application.Candidates.Queries;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Candidates.CommandsTests
{
    public class GetCandidateSkillsTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetCandidateSkills.Validator();
            validator.Validate(new GetCandidateSkills { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetCandidateSkills.Validator();
            validator.Validate(new GetCandidateSkills()).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetCandidateSkills.Handler(dbContext, AutoMapperHelper.Mapper);
            var skills = dbContext.CandidateSkills.Where(x => x.CandidateId == 1);

            var result = await handler.Handle(new GetCandidateSkills { Id = 1 }, CancellationToken.None);
            foreach (var model in result.Items)
            {
                skills.ShouldContain(x => x.SkillId == model.Id);
            }
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new GetCandidateSkills.Handler(dbContext, AutoMapperHelper.Mapper);
            await Should.ThrowAsync<NotFoundException>(async () =>
                    await handler.Handle(new GetCandidateSkills { Id = -1 }, CancellationToken.None));
        }
    }
}
