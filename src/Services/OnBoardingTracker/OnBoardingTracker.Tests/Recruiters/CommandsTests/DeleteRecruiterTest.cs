using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Recruiters.Commands;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Recruiters.CommandsTests
{
    public class DeleteRecruiterTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new DeleteRecruiter.Validator();
            validator.Validate(new DeleteRecruiter { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new DeleteRecruiter.Validator();
            validator.Validate(new DeleteRecruiter()).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_HasReference_ThrowsValidationException()
        {
            var handler = new DeleteRecruiter.Handler(dbContext, AutoMapperHelper.Mapper);

            var referenced = dbContext.Recruiters
                .Include(x => x.Vacancies)
                .First(s => s.Vacancies.Count > 0);

            var entities = await dbContext.Recruiters.ToListAsync();
            entities.First(x => x.Id == referenced.Id).IsDeleted.ShouldBeFalse();

            var result = handler.Handle(new DeleteRecruiter { Id = referenced.Id }, CancellationToken.None);
            await result.ShouldThrowAsync<ValidationException>();

            entities.First(x => x.Id == referenced.Id).IsDeleted.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new DeleteRecruiter.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.Recruiters.ToListAsync();
            entitiesBefore.Count.ShouldBe(4);

            var result = handler.Handle(new DeleteRecruiter { Id = -1 }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
            var entitiesAfter = await dbContext.Recruiters.ToListAsync();
            entitiesAfter.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new DeleteRecruiter.Handler(dbContext, AutoMapperHelper.Mapper);

            var entities = await dbContext.Recruiters
                .Include(x => x.Vacancies)
                .Include(x => x.Interviews).ToListAsync();
            var notReferenced = entities.First(s => !(s.Vacancies.Any() || s.Interviews.Any()));

            notReferenced.IsDeleted.ShouldBeFalse();

            var result = await handler.Handle(new DeleteRecruiter { Id = notReferenced.Id }, CancellationToken.None);

            notReferenced.IsDeleted.ShouldBeTrue();
        }
    }
}
