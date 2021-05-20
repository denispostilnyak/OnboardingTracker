using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.JobTypes.Commands;
using OnBoardingTracker.Application.JobTypes.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.JobTypes.CommandsTests
{
    public class DeleteJobTypeTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new DeleteJobType.Validator();
            validator.Validate(new DeleteJobType { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new DeleteJobType.Validator();
            validator.Validate(new DeleteJobType()).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_HasReference_ThrowsValidationException()
        {
            var handler = new DeleteJobType.Handler(dbContext, AutoMapperHelper.Mapper);

            var referenced = dbContext.JobTypes
                .Include(x => x.Vacancies)
                .First(s => s.Vacancies.Count > 0);

            referenced.IsDeleted.ShouldBeFalse();

            var result = handler.Handle(new DeleteJobType { Id = referenced.Id }, CancellationToken.None);
            await result.ShouldThrowAsync<ValidationException>();

            referenced.IsDeleted.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new DeleteJobType.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.JobTypes.ToListAsync();
            entitiesBefore.Count.ShouldBe(3);

            var result = handler.Handle(new DeleteJobType { Id = -1 }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
            var entitiesAfter = await dbContext.JobTypes.ToListAsync();
            entitiesAfter.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new DeleteJobType.Handler(dbContext, AutoMapperHelper.Mapper);

            var entities = dbContext.JobTypes.Include(x => x.Vacancies);
            var notReferenced = entities.First(s => !s.Vacancies.Any());

            notReferenced.IsDeleted.ShouldBeFalse();

            var result = await handler.Handle(new DeleteJobType { Id = notReferenced.Id }, CancellationToken.None);

            notReferenced.IsDeleted.ShouldBeTrue();
            dbContext.Vacancies.ShouldNotContain(x => x.JobTypeId == notReferenced.Id);
        }
    }
}
