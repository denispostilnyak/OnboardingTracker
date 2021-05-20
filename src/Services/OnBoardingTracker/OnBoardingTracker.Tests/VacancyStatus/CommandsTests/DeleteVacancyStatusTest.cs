using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.VacancyStatuses.Commands;
using OnBoardingTracker.Application.VacancyStatuses.Models;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.VacancyStatus.CommandsTests
{
    public class DeleteVacancyStatusTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new DeleteVacancyStatus.Validator();
            validator.Validate(new DeleteVacancyStatus
            {
                Id = 1
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new DeleteVacancyStatus.Validator();
            validator.Validate(new DeleteVacancyStatus
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new DeleteVacancyStatus.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.VacancyStatuses.ToListAsync();
            entitiesBefore.Count.ShouldBe(3);

            Func<Task> result = async () => await handler.Handle(new DeleteVacancyStatus { Id = -2 }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
            var entitiesAfter = await dbContext.VacancyStatuses.ToListAsync();
            entitiesAfter.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new DeleteVacancyStatus.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new DeleteVacancyStatus { Id = 3 }, CancellationToken.None);
            result.Id.ShouldBe(3);
            result.ShouldBeOfType<VacancyStatusModel>();
        }
    }
}
