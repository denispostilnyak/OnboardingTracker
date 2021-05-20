using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.Vacancy.Comands;
using OnBoardingTracker.Application.Vacancy.Models;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Vacancy.CommandsTests
{
    public class CreateVacancyTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateVacancy.Validator();
            validator.Validate(new CreateVacancy
            {
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
            var validator = new CreateVacancy.Validator();
            validator.Validate(new CreateVacancy
            {
                MaxSalary = -100,
                AssignedRecruiterId = 1,
                JobTypeId = 1,
                SeniorityLevelId = 1,
                WorkExperience = 2
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new CreateVacancy.Handler(dbContext, AutoMapperHelper.Mapper, fileStorage);

            var entitiesBefore = await dbContext.Vacancies.ToListAsync();
            entitiesBefore.Count.ShouldBe(3);
            entitiesBefore.ShouldNotContain(c => c.Title.Equals("test"));

            var result = await handler.Handle(
                new CreateVacancy
                {
                    Title = "test",
                    MaxSalary = 500,
                    AssignedRecruiterId = 1,
                    JobTypeId = 1,
                    SeniorityLevelId = 1,
                    VacancyStatusId = 1,
                    WorkExperience = 2
                }, CancellationToken.None);

            var entitiesAfter = await dbContext.Vacancies.ToListAsync();
            entitiesAfter.Count.ShouldBe(4);
            entitiesAfter.ShouldContain(c => c.Title.Equals("test"));
            result.ShouldBeOfType<VacancyModel>();
        }
    }
}
