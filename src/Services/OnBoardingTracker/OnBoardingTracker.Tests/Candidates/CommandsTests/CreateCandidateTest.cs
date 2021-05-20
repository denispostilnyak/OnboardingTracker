using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnBoardingTracker.Application.Candidates.Commands;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Candidates.CommandsTests
{
    public class CreateCandidateTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new CreateCandidate.Validator();
            validator.Validate(new CreateCandidate
            {
                FirstName = "Not Empty",
                LastName = "Not Empty",
                PhoneNumber = "NotEmpty",
                Email = "not@emp.ty",
                OriginId = 1,
                YearsOfExperience = 0,
                CurrentJobInformation = string.Empty,
                CvFileStream = new MemoryStream(Encoding.UTF8.GetBytes("whatever")),
                CvFileName = "whatever"
            }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new CreateCandidate.Validator();
            validator.Validate(new CreateCandidate
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                PhoneNumber = string.Empty,
                Email = string.Empty,
                OriginId = 0,
                YearsOfExperience = -1,
                CurrentJobInformation = string.Empty,
                CvFileName = string.Empty,
                CvFileStream = new MemoryStream()
            }).Errors.Count.ShouldBe(6);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var configuration = A.Fake<IConfiguration>();
            var handler = new CreateCandidate.Handler(dbContext, AutoMapperHelper.Mapper, fileStorage);

            var entitiesBefore = await dbContext.Candidates.ToListAsync();
            var previousCount = entitiesBefore.Count;
            entitiesBefore.ShouldNotContain(c => c.FirstName == "Not Empty");

            var result = await handler.Handle(
                new CreateCandidate
                {
                    FirstName = "Not Empty",
                    LastName = "Not Empty",
                    PhoneNumber = "NotEmpty",
                    Email = "not@emp.ty",
                    OriginId = 1,
                    YearsOfExperience = 0,
                    CurrentJobInformation = string.Empty
                },
                CancellationToken.None);

            var entitiesAfter = await dbContext.Candidates.ToListAsync();
            entitiesAfter.Count.ShouldBe(previousCount + 1);
            entitiesAfter.ShouldContain(c => c.FirstName.Equals("Not Empty"));
            result.ShouldBeOfType<CandidateModel>();
        }
    }
}
