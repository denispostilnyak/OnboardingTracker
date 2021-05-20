using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Candidates.Commands;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Candidates.CommandsTests
{
    public class DeleteCandidateTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new DeleteCandidate.Validator();
            validator.Validate(new DeleteCandidate { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative_IdIsZero()
        {
            var validator = new DeleteCandidate.Validator();
            validator.Validate(new DeleteCandidate()).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_HasReference_ThrowsValidationException()
        {
            var handler = new DeleteCandidate.Handler(dbContext, AutoMapperHelper.Mapper);

            var referenced = dbContext.Candidates
                .Include(x => x.Interviews)
                .First(s => s.Interviews.Count > 0);

            referenced.IsDeleted.ShouldBeFalse();

            var result = handler.Handle(new DeleteCandidate { Id = referenced.Id }, CancellationToken.None);
            await result.ShouldThrowAsync<ValidationException>();

            referenced.IsDeleted.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new DeleteCandidate.Handler(dbContext, AutoMapperHelper.Mapper);
            var entityCount = dbContext.Candidates.Count();

            var result = handler.Handle(new DeleteCandidate { Id = -1 }, CancellationToken.None);

            await result.ShouldThrowAsync<NotFoundException>();
            dbContext.Candidates.Count().ShouldBe(entityCount);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new DeleteCandidate.Handler(dbContext, AutoMapperHelper.Mapper);

            var entities = dbContext.Candidates.Include(x => x.Interviews);
            var notReferenced = entities.First(s => !s.Interviews.Any());

            notReferenced.IsDeleted.ShouldBeFalse();

            var result = await handler.Handle(new DeleteCandidate { Id = notReferenced.Id }, CancellationToken.None);

            notReferenced.IsDeleted.ShouldBeTrue();
            dbContext.CandidateSkills.ShouldNotContain(x => x.CandidateId == notReferenced.Id);
            dbContext.Interviews.ShouldNotContain(x => x.CandidateId == notReferenced.Id);
        }
    }
}
