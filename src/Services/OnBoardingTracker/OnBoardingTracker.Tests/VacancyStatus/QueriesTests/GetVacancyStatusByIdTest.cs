using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.VacancyStatuses.Queries;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.VacancyStatus.QueriesTests
{
    public class GetVacancyStatusByIdTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetVacancyStatusById.Validator();
            validator.Validate(new GetVacancyStatusById { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetVacancyStatusById.Validator();
            validator.Validate(new GetVacancyStatusById { }).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetVacancyStatusById.Handler(dbContext, AutoMapperHelper.Mapper);

            var vacancyStatus = await dbContext.VacancyStatuses.FirstOrDefaultAsync(vacancyStatus => vacancyStatus.Id == 1);

            var result = await handler.Handle(new GetVacancyStatusById { Id = 1 }, CancellationToken.None);

            result.Id.ShouldBe(vacancyStatus.Id);
        }

        [Fact]
        public async Task Handle_Negative()
        {
            var handler = new GetVacancyStatusById.Handler(dbContext, AutoMapperHelper.Mapper);

            var result = await handler.Handle(new GetVacancyStatusById { Id = -1 }, CancellationToken.None);

            result.ShouldBeNull();
        }
    }
}
