using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.Vacancy.Comands;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Vacancy.CommandsTests
{
    public class AssignSkillsToVacancyTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new AssignSkillsToVacancy.Validator();
            validator.Validate(new AssignSkillsToVacancy
            {
                VacancyId = 1,
                SkillIdList = new List<int> { 1, 2 }
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new AssignSkillsToVacancy.Validator();
            validator.Validate(new AssignSkillsToVacancy
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new AssignSkillsToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            var entitiesBefore = await dbContext.VacancySkills.ToListAsync();
            entitiesBefore.Count.ShouldBe(4);
            entitiesBefore.ShouldNotContain(c => c.VacancyId == 1 && c.SkillId == 3);

            var result = await handler.Handle(
                new AssignSkillsToVacancy
                {
                    VacancyId = 1,
                    SkillIdList = new List<int> { 3 }
                }, CancellationToken.None);

            var entitiesAfter = await dbContext.VacancySkills.ToListAsync();
            entitiesAfter.Count.ShouldBe(5);
            entitiesAfter.ShouldContain(c => c.VacancyId == 1 && c.SkillId == 3);
            result.ShouldBeOfType<SkillList>();
        }

        [Fact]
        public async Task Handle_Negative_Not_Exist_SkillsVacancy()
        {
            var handler = new AssignSkillsToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(
                new AssignSkillsToVacancy
                {
                    VacancyId = 1,
                    SkillIdList = new List<int> { 2 }
                }, CancellationToken.None);

            await result.ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_Negative_Not_Exist_Vacancy()
        {
            var handler = new AssignSkillsToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(
                new AssignSkillsToVacancy
                {
                    VacancyId = -3,
                    SkillIdList = new List<int> { 3 }
                }, CancellationToken.None);

            await result.ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_Negative_Not_Exist_Candidate()
        {
            var handler = new AssignSkillsToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(
                new AssignSkillsToVacancy
                {
                    VacancyId = 1,
                    SkillIdList = new List<int> { -2 }
                }, CancellationToken.None);

            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
