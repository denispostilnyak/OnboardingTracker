using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Infrastructure.Models;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.Vacancy.Comands;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.Tests.Vacancy.CommandsTests
{
    public class AssignCandidatesToVacancyTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new AssignCandidatesToVacancy.Validator();
            validator.Validate(new AssignCandidatesToVacancy
            {
                VacancyId = 1,
                CandidatesIdList = new List<int> { 1, 2 }
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new AssignCandidatesToVacancy.Validator();
            validator.Validate(new AssignCandidatesToVacancy
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new AssignCandidatesToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            var entitiesBefore = await dbContext.CandidateVacancies.ToListAsync();
            entitiesBefore.Count.ShouldBe(5);
            entitiesBefore.ShouldNotContain(c => c.VacancyId == 2 && c.CandidateId == 3);

            var result = await handler.Handle(
                new AssignCandidatesToVacancy
                {
                    VacancyId = 2,
                    CandidatesIdList = new List<int> { 3 }
                }, CancellationToken.None);

            var entitiesAfter = await dbContext.CandidateVacancies.ToListAsync();
            entitiesAfter.Count.ShouldBe(6);
            entitiesAfter.ShouldContain(c => c.VacancyId == 2 && c.CandidateId == 3);
            result.ShouldBeOfType<ItemsCollection<CandidateModel>>();
        }

        [Fact]
        public async Task Handle_Negative_Not_Exist_CandidateVacancy()
        {
            var handler = new AssignCandidatesToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(
                new AssignCandidatesToVacancy
                {
                    VacancyId = 1,
                    CandidatesIdList = new List<int> { 1 }
                }, CancellationToken.None);

           await result.ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_Negative_Not_Exist_Vacancy()
        {
            var handler = new AssignCandidatesToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(
                new AssignCandidatesToVacancy
                {
                    VacancyId = -3,
                    CandidatesIdList = new List<int> { 3 }
                }, CancellationToken.None);

            await result.ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_Negative_Not_Exist_Candidate()
        {
            var handler = new AssignCandidatesToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(
                new AssignCandidatesToVacancy
                {
                    VacancyId = 1,
                    CandidatesIdList = new List<int> { -2 }
                }, CancellationToken.None);

            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
