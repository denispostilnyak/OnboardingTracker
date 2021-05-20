using Microsoft.EntityFrameworkCore;
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
    public class CreateVacancyStatusTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateVacancyStatus.Validator();
            validator.Validate(new CreateVacancyStatus
            {
                Name = "Test"
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new CreateVacancyStatus.Validator();
            validator.Validate(new CreateVacancyStatus
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new CreateVacancyStatus.Handler(dbContext, AutoMapperHelper.Mapper);

            var entitiesBefore = await dbContext.VacancyStatuses.ToListAsync();
            entitiesBefore.Count.ShouldBe(3);
            entitiesBefore.ShouldNotContain(c => c.Name.Equals("test"));

            var result = await handler.Handle(
                new CreateVacancyStatus
                {
                    Name = "test"
                }, CancellationToken.None);

            var entitiesAfter = await dbContext.VacancyStatuses.ToListAsync();
            entitiesAfter.Count.ShouldBe(4);
            entitiesAfter.ShouldContain(c => c.Name.Equals("test"));
            result.ShouldBeOfType<VacancyStatusModel>();
        }
    }
}
