using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Candidates.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Candidates.QueriesTests
{
    public class GetCandidateByIdTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetCandidateById.Validator();
            validator.Validate(new GetCandidateById { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative_EmptyId()
        {
            var validator = new GetCandidateById.Validator();
            validator.Validate(new GetCandidateById { Id = 0 }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var mapper = AutoMapperHelper.Mapper;
            int candidateId = 1;
            var handler = new GetCandidateById.Handler(dbContext, mapper);

            var expected = mapper.Map<CandidateModel>(dbContext.Candidates.First(x => x.Id == candidateId));
            expected.ShouldNotBeNull();

            var actual = await handler.Handle(new GetCandidateById { Id = candidateId }, CancellationToken.None);
            actual.Id.ShouldBe(expected.Id);
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ReturnsIsNull()
        {
            var handler = new GetCandidateById.Handler(dbContext, AutoMapperHelper.Mapper);

            var actual = await handler.Handle(new GetCandidateById { Id = -1 }, CancellationToken.None);
            actual.ShouldBeNull();
        }
    }
}
