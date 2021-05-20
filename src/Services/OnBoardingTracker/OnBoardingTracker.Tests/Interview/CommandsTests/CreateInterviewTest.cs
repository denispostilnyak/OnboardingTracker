using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnBoardingTracker.Application.Interviews.Commands;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Infrastructure.EmailService.Abstract;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Interview.CommandsTests
{
    public class CreateInterviewTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateInterview.Validator();
            validator.Validate(new CreateInterview
            {
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
            var validator = new CreateInterview.Validator();
            validator.Validate(new CreateInterview
            {
                CandidateId = 1,
                VacancyId = 1,
                RecruiterId = 1
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var emailService = A.Fake<IEmailService>();
            var configuration = A.Fake<IConfiguration>();
            var handler = new CreateInterview.Handler(dbContext, AutoMapperHelper.Mapper, emailService, configuration);

            var entitiesBefore = await dbContext.Interviews.ToListAsync();
            entitiesBefore.Count.ShouldBe(3);
            entitiesBefore.ShouldNotContain(c => c.Title.Equals("test"));

            var result = await handler.Handle(
                new CreateInterview
                {
                    Title = "test",
                    CandidateId = 1,
                    VacancyId = 1,
                    RecruiterId = 1,
                    EndingTime = DateTime.UtcNow,
                    StartingTime = DateTime.UtcNow
                }, CancellationToken.None);

            var entitiesAfter = await dbContext.Interviews.ToListAsync();
            entitiesAfter.Count.ShouldBe(4);
            entitiesAfter.ShouldContain(c => c.Title.Equals("test"));
            result.ShouldBeOfType<InterviewModel>();
        }
    }
}
