using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.Vacancy.Queries;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Vacancy.QueriesTests
{
    public class GetVacanciesFilterTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetVacanciesFilter.Validator();
            validator.Validate(new GetVacanciesFilter { Page = 1, Limit = 10 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetVacanciesFilter.Validator();
            validator.Validate(new GetVacanciesFilter { }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetVacanciesFilter.Handler(dbContext, AutoMapperHelper.Mapper);

            var vacancy = await dbContext.Vacancies.Where(vacancy => vacancy.AssignedRecruiterId == 1).Take(5).ToListAsync();

            var result = await handler.Handle(new GetVacanciesFilter { Page = 1, Limit = 5, AssignedRecruiterId = 1 }, CancellationToken.None);

            result.Count.ShouldBe(vacancy.Count);
        }
    }
}
