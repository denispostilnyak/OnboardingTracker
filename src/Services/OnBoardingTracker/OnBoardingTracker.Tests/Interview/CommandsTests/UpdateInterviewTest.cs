using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Interviews.Commands;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Interview.CommandsTests
{
    public class UpdateInterviewTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new UpdateInterview.Validator();
            validator.Validate(new UpdateInterview
            {
                Id = 2,
                Title = "Test",
                CandidateId = 1,
                VacancyId = 1,
                RecruiterId = 1,
                EndingTime = DateTime.UtcNow.AddDays(1),
                StartingTime = DateTime.UtcNow
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new UpdateInterview.Validator();
            validator.Validate(new UpdateInterview
            {
                Id = -2,
                Title = "Test",
                CandidateId = 1,
                VacancyId = 1,
                EndingTime = DateTime.UtcNow,
                StartingTime = DateTime.UtcNow
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new UpdateInterview.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(
                new UpdateInterview
                {
                    Id = 2,
                    Title = "Test",
                    CandidateId = 1,
                    VacancyId = 1,
                    RecruiterId = 1,
                    EndingTime = DateTime.UtcNow,
                    StartingTime = DateTime.UtcNow
                }, CancellationToken.None);
            var entities = await dbContext.Interviews.ToListAsync();
            entities.ShouldContain(c => c.Title.Equals("Test"));
            result.ShouldBeOfType<InterviewModel>();
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new UpdateInterview.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(
                new UpdateInterview
            {
                Id = -2,
                Title = "Test",
                CandidateId = 1,
                VacancyId = 1,
                RecruiterId = 1,
                EndingTime = DateTime.UtcNow,
                StartingTime = DateTime.UtcNow
            }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
