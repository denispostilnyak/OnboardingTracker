using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
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
    public class DeleteSkillTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new DeleteSkill.Validator();
            validator.Validate(new DeleteSkill
            {
                Id = 1
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new DeleteSkill.Validator();
            validator.Validate(new DeleteSkill
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new DeleteSkill.Handler(dbContext, AutoMapperHelper.Mapper);
            var entitiesBefore = await dbContext.Skills.ToListAsync();
            entitiesBefore.Count.ShouldBe(5);

            Func<Task> result = async () => await handler.Handle(new DeleteSkill { Id = -2 }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
            var entitiesAfter = await dbContext.Skills.ToListAsync();
            entitiesAfter.Count.ShouldBe(5);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new DeleteSkill.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new DeleteSkill { Id = 3 }, CancellationToken.None);
            result.Id.ShouldBe(3);
            result.ShouldBeOfType<SkillModel>();
        }
    }
}
