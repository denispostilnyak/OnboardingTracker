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
    public class DeleteInterviewTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new DeleteInterview.Validator();
            validator.Validate(new DeleteInterview
            {
                Id = 1
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new DeleteInterview.Validator();
            validator.Validate(new DeleteInterview
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new DeleteInterview.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.Interviews.ToListAsync();
            entitiesBefore.Count.ShouldBe(3);

            Func<Task> result = async () => await handler.Handle(new DeleteInterview { Id = -2 }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
            var entitiesAfter = await dbContext.Interviews.ToListAsync();
            entitiesAfter.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new DeleteInterview.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new DeleteInterview { Id = 3 }, CancellationToken.None);
            result.Id.ShouldBe(3);
            result.ShouldBeOfType<InterviewModel>();
        }
    }
}
