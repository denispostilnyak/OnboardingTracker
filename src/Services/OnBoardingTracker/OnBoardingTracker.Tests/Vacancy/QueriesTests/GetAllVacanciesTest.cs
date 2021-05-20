using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Tests.Helpers;
using OnBoardingTracker.Application.Vacancy.Queries;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Vacancy.QueriesTests
{
    public class GetAllVacanciesTest : BaseTest
    {
        [Fact]
        public async Task Handle_Posititve()
        {
            var handler = new GetVacancies.Handler(dbContext, AutoMapperHelper.Mapper);

            var vacanciesList = await dbContext.Vacancies.ToListAsync();

            var result = await handler.Handle(new GetVacancies { }, CancellationToken.None);

            result.Items.Count().ShouldBe(vacanciesList.Count);
        }
    }
}
