using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.Vacancy.Comands;
using OnBoardingTracker.Application.Vacancy.Models;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Vacancy.CommandsTests
{
    public class DeleteVacancyTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new DeleteVacancy.Validator();
            validator.Validate(new DeleteVacancy
            {
                Id = 1
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new DeleteVacancy.Validator();
            validator.Validate(new DeleteVacancy
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative_Exist_In_Interviews()
        {
            var handler = new DeleteVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(new DeleteVacancy { Id = 1 }, CancellationToken.None);
            await result.ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new DeleteVacancy.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.Vacancies.ToListAsync();
            entitiesBefore.Count.ShouldBe(3);

            Func<Task> result = async () => await handler.Handle(new DeleteVacancy { Id = -2 }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
            var entitiesAfter = await dbContext.Vacancies.ToListAsync();
            entitiesAfter.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new DeleteVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new DeleteVacancy { Id = 3 }, CancellationToken.None);
            result.Id.ShouldBe(3);
            result.ShouldBeOfType<VacancyModel>();
        }
    }
}
