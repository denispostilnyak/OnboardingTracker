using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.VacancyStatuses.Commands;
using OnBoardingTracker.Application.VacancyStatuses.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.VacancyStatus.CommandsTests
{
    public class UpdateVacancyStatusTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new UpdateVacancyStatus.Validator();
            validator.Validate(new UpdateVacancyStatus
            {
                Id = 1,
                Name = "Pending"
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new UpdateVacancyStatus.Validator();
            validator.Validate(new UpdateVacancyStatus
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var vacancyModel = new UpdateVacancyStatus
            {
                Id = 1,
                Name = "Pending"
            };
            var handler = new UpdateVacancyStatus.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new UpdateVacancyStatus { Id = 1, Name = "Pending" }, CancellationToken.None);
            var entities = await dbContext.VacancyStatuses.ToListAsync();
            entities.ShouldContain(c => c.Name.Equals("Pending") && c.Id == 1);
            result.ShouldBeOfType<VacancyStatusModel>();
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new UpdateVacancyStatus.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(new UpdateVacancyStatus { Id = -1, Name = "Pending" }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
