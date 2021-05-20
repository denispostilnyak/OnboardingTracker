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
    public class UpdateSkillTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new UpdateSkill.Validator();
            validator.Validate(new UpdateSkill
            {
                Id = 1,
                Name = "net"
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new UpdateSkill.Validator();
            validator.Validate(new UpdateSkill
            {
            }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var vacancyModel = new UpdateSkill
            {
                Id = 1,
                Name = "net"
            };
            var handler = new UpdateSkill.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new UpdateSkill { Id = 1, Name = "net" }, CancellationToken.None);
            var entities = await dbContext.Skills.ToListAsync();
            entities.ShouldContain(c => c.Name.Equals("net") && c.Id == 1);
            result.ShouldBeOfType<SkillModel>();
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new UpdateSkill.Handler(dbContext, AutoMapperHelper.Mapper);

            Func<Task> result = async () => await handler.Handle(new UpdateSkill { Id = -1, Name = "net" }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
