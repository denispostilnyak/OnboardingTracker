using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Interviews.Commands;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Interview.CommandsTests
{
    public class AssignInterviewToVacancyTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new AssignInterviewToVacancy.Validator();
            validator.Validate(new AssignInterviewToVacancy
            {
                InterviewId = 1,
                VacancyId = 1
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new AssignInterviewToVacancy.Validator();
            validator.Validate(new AssignInterviewToVacancy
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new AssignInterviewToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            var entitiesBefore = await dbContext.Interviews.ToListAsync();
            entitiesBefore.ShouldNotContain(c => c.VacancyId == 2 && c.Id == 3);

            var result = await handler.Handle(
                new AssignInterviewToVacancy
                {
                    VacancyId = 2,
                    InterviewId = 3
                }, CancellationToken.None);

            var entitiesAfter = await dbContext.Interviews.ToListAsync();
            entitiesAfter.ShouldContain(c => c.VacancyId == 2 && c.Id == 3);
            result.ShouldBeOfType<InterviewModel>();
        }

        [Fact]
        public async Task Handle_Negative_Not_Exist_Vacancy()
        {
            var handler = new AssignInterviewToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(
                new AssignInterviewToVacancy
                {
                    VacancyId = -5,
                    InterviewId = 3
                }, CancellationToken.None);

            await result.ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_Negative_Not_Exist_Interview()
        {
            var handler = new AssignInterviewToVacancy.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(
                new AssignInterviewToVacancy
                {
                    VacancyId = 1,
                    InterviewId = -3
                }, CancellationToken.None);

            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
