using OnBoardingTracker.Application.Candidates.Commands;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Candidates.CommandsTests
{
    public class UpdateCandidateTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new UpdateCandidate.Validator();
            validator.Validate(new UpdateCandidate
            {
                Id = 1,
                FirstName = "Updated",
                LastName = "Updated",
                PhoneNumber = "Updated",
                OriginId = 2,
                CurrentJobInformation = string.Empty,
                Email = "upd@at.ed",
                YearsOfExperience = 1,
                FileStream = new MemoryStream(Encoding.UTF8.GetBytes("whatever")),
                FileName = "whatever"
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new UpdateCandidate.Validator();
            validator.Validate(new UpdateCandidate
            {
                Id = 0,
                FirstName = string.Empty,
                LastName = string.Empty,
                PhoneNumber = string.Empty,
                OriginId = 0,
                CurrentJobInformation = string.Empty,
                Email = string.Empty,
                YearsOfExperience = -1,
                FileName = string.Empty,
                FileStream = new MemoryStream()
            }).Errors.Count.ShouldBe(6);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new UpdateCandidate.Handler(dbContext, AutoMapperHelper.Mapper, fileStorage);
            var result = await handler.Handle(
                new UpdateCandidate
                {
                    Id = 1,
                    FirstName = "Updated",
                    LastName = "Updated",
                    PhoneNumber = "Updated",
                    Email = "upd@at.ed",
                    OriginId = 2,
                    CurrentJobInformation = string.Empty,
                    YearsOfExperience = 2
                }, CancellationToken.None);
            dbContext.Candidates.ShouldContain(c => c.FirstName == "Updated");
            result.ShouldBeOfType<CandidateModel>();
        }

        [Fact]
        public async Task Handle_Negative_InvalidIdThrowsNotFoundException()
        {
            var handler = new UpdateCandidate.Handler(dbContext, AutoMapperHelper.Mapper, fileStorage);
            var resultTask = handler.Handle(
                new UpdateCandidate
                {
                    Id = -1,
                    FirstName = "Updated",
                    LastName = "Updated",
                    PhoneNumber = "Updated",
                    Email = "upd@at.ed",
                    OriginId = 2,
                    CurrentJobInformation = string.Empty,
                    YearsOfExperience = 2
                }, CancellationToken.None);

            await resultTask.ShouldThrowAsync<NotFoundException>();
        }
    }
}
