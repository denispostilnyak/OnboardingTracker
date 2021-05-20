using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.Vacancy.Comands;
using OnBoardingTracker.Application.Vacancy.Models;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Vacancy.CommandsTests
{
    public class UpdateVacancyTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new UpdateVacancy.Validator();
            validator.Validate(new UpdateVacancy
            {
                Id = 2,
                Title = "Test",
                Description = "Here Description",
                MaxSalary = 500,
                AssignedRecruiterId = 1,
                JobTypeId = 1,
                SeniorityLevelId = 1,
                VacancyStatusId = 1,
                WorkExperience = 2
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new UpdateVacancy.Validator();
            validator.Validate(new UpdateVacancy
            {
                Id = -3,
                Title = "Test",
                MaxSalary = 500,
                JobTypeId = 1,
                SeniorityLevelId = 1,
                VacancyStatusId = 1,
                WorkExperience = 2
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new UpdateVacancy.Handler(dbContext, AutoMapperHelper.Mapper, fileStorage);

            var result = await handler.Handle(
                new UpdateVacancy
                {
                    Id = 2,
                    Title = "Test",
                    MaxSalary = 500,
                    AssignedRecruiterId = 1,
                    JobTypeId = 1,
                    SeniorityLevelId = 1,
                    VacancyStatusId = 1,
                    WorkExperience = 2
                }, CancellationToken.None);
            var entities = await dbContext.Vacancies.ToListAsync();
            entities.ShouldContain(c => c.Title.Equals("Test") && c.MaxSalary == 500);
            result.ShouldBeOfType<VacancyModel>();
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var vacancyModel = new VacancyModel
            {
                Id = -1,
                Title = "Test",
                MaxSalary = 500,
                AssignedRecruiterId = 1,
                JobTypeId = 1,
                SeniorityLevelId = 1,
                VacancyStatusId = 1,
                WorkExperience = 2
            };
            var handler = new UpdateVacancy.Handler(dbContext, AutoMapperHelper.Mapper, fileStorage);

            Func<Task> result = async () => await handler.Handle(
                new UpdateVacancy
                {
                    Id = -1,
                    Title = "Test",
                    MaxSalary = 500,
                    AssignedRecruiterId = 1,
                    JobTypeId = 1,
                    SeniorityLevelId = 1,
                    VacancyStatusId = 1,
                    WorkExperience = 2
                }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
