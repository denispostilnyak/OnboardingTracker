using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Skills.Commands;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Skill.CommandsTests
{
    public class CreateSkillTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateSkill.Validator();
            validator.Validate(new CreateSkill
            {
                Name = "Test"
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new CreateSkill.Validator();
            validator.Validate(new CreateSkill
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new CreateSkill.Handler(dbContext, AutoMapperHelper.Mapper);

            var entitiesBefore = await dbContext.Skills.ToListAsync();
            entitiesBefore.Count.ShouldBe(5);
            entitiesBefore.ShouldNotContain(c => c.Name.Equals("test"));

            var result = await handler.Handle(
                new CreateSkill
                {
                    Name = "test"
                }, CancellationToken.None);

            var entitiesAfter = await dbContext.Skills.ToListAsync();
            entitiesAfter.Count.ShouldBe(6);
            entitiesAfter.ShouldContain(c => c.Name.Equals("test"));
            result.ShouldBeOfType<SkillModel>();
        }
    }
}
