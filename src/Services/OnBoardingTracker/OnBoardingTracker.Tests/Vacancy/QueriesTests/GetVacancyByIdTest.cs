using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.Vacancy.Queries;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Vacancy.QueriesTests
{
    public class GetVacancyByIdTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetVacancyById.Validator();
            validator.Validate(new GetVacancyById { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetVacancyById.Validator();
            validator.Validate(new GetVacancyById { }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetVacancyById.Handler(dbContext, AutoMapperHelper.Mapper);

            var vacancy = await dbContext.Vacancies.FirstOrDefaultAsync(vacancy => vacancy.Id == 1);

            var result = await handler.Handle(new GetVacancyById { Id = 1 }, CancellationToken.None);

            result.Id.ShouldBe(vacancy.Id);
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new GetVacancyById.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new GetVacancyById { Id = -1 }, CancellationToken.None);

            result.ShouldBeNull();
        }
    }
}
