using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.VacancyStatuses.Queries;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.VacancyStatus.QueriesTests
{
    public class GetVacancyStatusesTest : BaseTest
    {
        [Fact]
        public async Task Handle_Posititve()
        {
            var handler = new GetVacancyStatuses.Handler(AutoMapperHelper.Mapper, dbContext);

            var vacanciesStatusesList = await dbContext.VacancyStatuses.ToListAsync();

            var result = await handler.Handle(new GetVacancyStatuses { }, CancellationToken.None);

            result.Items.Count().ShouldBe(vacanciesStatusesList.Count);
        }
    }
}
