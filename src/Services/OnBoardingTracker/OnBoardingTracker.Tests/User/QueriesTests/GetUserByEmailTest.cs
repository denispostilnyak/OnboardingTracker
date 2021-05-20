using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.User.Queries;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.User.QueriesTests
{
    public class GetUserByEmailTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetUserByEmail.Validator();
            validator.Validate(new GetUserByEmail { Email = "test@gmail.com" }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetUserByEmail.Validator();
            validator.Validate(new GetUserByEmail { Email = null }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetUserByEmail.Handler(dbContext, AutoMapperHelper.Mapper);

            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == "sergey.kudriashov@devoxsoftware.com");

            var result = await handler.Handle(new GetUserByEmail { Email = "sergey.kudriashov@devoxsoftware.com" }, CancellationToken.None);

            result.Email.ShouldBe(user.Email);
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new GetUserByEmail.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new GetUserByEmail { Email = "testWrong@gmail.com" }, CancellationToken.None);

            result.ShouldBeNull();
        }
    }
}
