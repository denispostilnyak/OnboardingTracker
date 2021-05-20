using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.SeniorityLevels.Commands;
using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.SeniorityLevels.CommandsTests
{
    public class DeleteSeniorityLevelTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new DeleteSeniorityLevel.Validator();
            validator.Validate(new DeleteSeniorityLevel { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new DeleteSeniorityLevel.Validator();
            validator.Validate(new DeleteSeniorityLevel()).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_HasReference_ThrowsValidationException()
        {
            var handler = new DeleteSeniorityLevel.Handler(dbContext, AutoMapperHelper.Mapper);

            var referenced = dbContext.SeniorityLevels
                .Include(x => x.Vacancies)
                .First(s => s.Vacancies.Count > 0);

            var entitiesBefore = await dbContext.SeniorityLevels.ToListAsync();
            entitiesBefore.First(x => x.Id == referenced.Id).IsDeleted.ShouldBeFalse();

            var result = handler.Handle(new DeleteSeniorityLevel { Id = referenced.Id }, CancellationToken.None);
            await result.ShouldThrowAsync<ValidationException>();

            entitiesBefore.First(x => x.Id == referenced.Id).IsDeleted.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new DeleteSeniorityLevel.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.SeniorityLevels.ToListAsync();
            entitiesBefore.Count.ShouldBe(4);

            var result = handler.Handle(new DeleteSeniorityLevel { Id = -1 }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
            var entitiesAfter = await dbContext.SeniorityLevels.ToListAsync();
            entitiesAfter.Count.ShouldBe(4);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new DeleteSeniorityLevel.Handler(dbContext, AutoMapperHelper.Mapper);

            var notReferenced = dbContext.SeniorityLevels
                .Include(x => x.Vacancies)
                .First(s => s.Vacancies.Count == 0);

            var entitiesBefore = await dbContext.SeniorityLevels.ToListAsync();
            entitiesBefore.First(x => x.Id == notReferenced.Id).IsDeleted.ShouldBeFalse();

            var result = await handler.Handle(new DeleteSeniorityLevel { Id = notReferenced.Id }, CancellationToken.None);

            entitiesBefore.First(x => x.Id == notReferenced.Id).IsDeleted.ShouldBeTrue();
        }
    }
}
