using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Origins.Commands;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Origins.CommandTests
{
    public class DeleteOriginTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new DeleteOrigin.Validator();
            validator.Validate(new DeleteOrigin { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new DeleteOrigin.Validator();
            validator.Validate(new DeleteOrigin()).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_HasReference_ThrowsValidationException()
        {
            var handler = new DeleteOrigin.Handler(dbContext, AutoMapperHelper.Mapper);

            var referenced = dbContext.CandidateOrigins
                .Include(x => x.Candidates)
                .First(s => s.Candidates.Count > 0);

            referenced.IsDeleted.ShouldBeFalse();

            var result = handler.Handle(new DeleteOrigin { Id = referenced.Id }, CancellationToken.None);
            await result.ShouldThrowAsync<ValidationException>();

            referenced.IsDeleted.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new DeleteOrigin.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.CandidateOrigins.ToListAsync();
            entitiesBefore.Count.ShouldBe(3);

            var result = handler.Handle(new DeleteOrigin { Id = -1 }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
            var entitiesAfter = await dbContext.CandidateOrigins.ToListAsync();
            entitiesAfter.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new DeleteOrigin.Handler(dbContext, AutoMapperHelper.Mapper);

            var entities = dbContext.CandidateOrigins.Include(x => x.Candidates);
            var notReferenced = entities.First(s => !s.Candidates.Any());

            notReferenced.IsDeleted.ShouldBeFalse();

            var result = await handler.Handle(new DeleteOrigin { Id = notReferenced.Id }, CancellationToken.None);

            notReferenced.IsDeleted.ShouldBeTrue();
        }
    }
}
