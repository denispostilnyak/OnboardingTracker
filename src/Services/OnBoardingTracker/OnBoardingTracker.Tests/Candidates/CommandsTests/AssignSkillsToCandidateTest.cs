using OnBoardingTracker.Application.Candidates.Commands;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Candidates.CommandsTests
{
    public class AssignSkillsToCandidateTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new AssignCandidateSkills.Validator();
            validator
                .Validate(new AssignCandidateSkills { CandidateId = 1, SkillIds = new List<int> { 1 } })
                .IsValid
                .ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative_EmptySkillList()
        {
            var validator = new AssignCandidateSkills.Validator();
            validator
                .Validate(new AssignCandidateSkills { CandidateId = 1, SkillIds = new List<int>() })
                .IsValid
                .ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new AssignCandidateSkills.Handler(dbContext, AutoMapperHelper.Mapper);
            dbContext.CandidateSkills.Count().ShouldBe(6);
            var result = await handler.Handle(
                new AssignCandidateSkills
                {
                    CandidateId = 1,
                    SkillIds = new List<int> { 1 }
                }, CancellationToken.None);
            dbContext.CandidateSkills.Count().ShouldBe(7);
            var entitiesAfter = dbContext.CandidateSkills.Where(x => x.CandidateId == 1);
            entitiesAfter.ShouldContain(x => x.SkillId == 1);
        }

        [Fact]
        public async void Handle_Negative_InvalidCandidateId_ThrowsValidationException()
        {
            var handler = new AssignCandidateSkills.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = handler.Handle(
                new AssignCandidateSkills
                {
                    CandidateId = -1,
                    SkillIds = new List<int> { 1 }
                }, CancellationToken.None);
            await result.ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_Negative_SkillDoesNotExist_ThrowsValidationException()
        {
            var handler = new AssignCandidateSkills.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = handler.Handle(
                new AssignCandidateSkills
                {
                    CandidateId = 1,
                    SkillIds = new List<int> { -1 }
                }, CancellationToken.None);
            await result.ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_Negative_SkillAlreadyAttached_ThrowsValidationException()
        {
            var handler = new AssignCandidateSkills.Handler(dbContext, AutoMapperHelper.Mapper);
            await handler.Handle(
                new AssignCandidateSkills
                {
                    CandidateId = 1,
                    SkillIds = new List<int> { 1 }
                }, CancellationToken.None);

            var result = handler.Handle(
                new AssignCandidateSkills
                {
                    CandidateId = 1,
                    SkillIds = new List<int> { 1 }
                }, CancellationToken.None);

            await result.ShouldThrowAsync<ValidationException>();
        }
    }
}
