using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.User.Commands;
using OnBoardingTracker.Application.User.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.User.CommandsTests
{
    public class CreateUserTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateUser.Validator();
            validator.Validate(new CreateUser
            {
                FirstName = "Test",
                LastName = "Testovich",
                Email = "test@gmail.com"
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new CreateUser.Validator();
            validator.Validate(new CreateUser
            {
                FirstName = "Test",
                LastName = "Testovich"
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new CreateUser.Handler(dbContext, AutoMapperHelper.Mapper);

            var entitiesBefore = await dbContext.Users.ToListAsync();
            entitiesBefore.Count.ShouldBe(3);
            entitiesBefore.ShouldNotContain(c => c.Email.Equals("test@gmail.com"));

            var result = await handler.Handle(
                new CreateUser
                {
                    FirstName = "Test",
                    LastName = "Testovich",
                    Email = "test@gmail.com"
                }, CancellationToken.None);

            var entitiesAfter = await dbContext.Users.ToListAsync();
            entitiesAfter.Count.ShouldBe(4);
            entitiesAfter.ShouldContain(c => c.Email.Equals("test@gmail.com"));
            result.ShouldBeOfType<UserModel>();
        }
    }
}
