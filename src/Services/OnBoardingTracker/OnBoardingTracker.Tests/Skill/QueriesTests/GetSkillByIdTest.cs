using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Skills.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Skill.QueriesTests
{
    public class GetSkillByIdTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetSkillById.Validator();
            validator.Validate(new GetSkillById { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetSkillById.Validator();
            validator.Validate(new GetSkillById { }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetSkillById.Handler(dbContext, AutoMapperHelper.Mapper);

            var skillStatus = await dbContext.Skills.FirstOrDefaultAsync(skill => skill.Id == 1);

            var result = await handler.Handle(new GetSkillById { Id = 1 }, CancellationToken.None);

            result.Id.ShouldBe(skillStatus.Id);
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new GetSkillById.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new GetSkillById { Id = -1 }, CancellationToken.None);

            result.ShouldBeNull();
        }
    }
}
