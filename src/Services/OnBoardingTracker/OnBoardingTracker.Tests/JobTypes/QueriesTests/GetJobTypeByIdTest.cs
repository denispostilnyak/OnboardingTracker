using OnBoardingTracker.Application.JobTypes.Models;
using OnBoardingTracker.Application.JobTypes.Queries;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.JobTypes.QueriesTests
{
    public class GetJobTypeByIdTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new GetJobTypeById.Validator();
            validator.Validate(new GetJobTypeById { Id = 1 }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new GetJobTypeById.Validator();
            validator.Validate(new GetJobTypeById()).IsValid.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new GetJobTypeById.Handler(dbContext, AutoMapperHelper.Mapper);
            var entity = dbContext.JobTypes.First(x => x.Id == 1);
            var result = await handler.Handle(new GetJobTypeById { Id = 1 }, CancellationToken.None);
            result.Name.ShouldBe(entity.Name);
            result.ShouldBeOfType<JobTypeModel>();
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ReturnsNull()
        {
            var handler = new GetJobTypeById.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = await handler.Handle(new GetJobTypeById { Id = -1 }, CancellationToken.None);
            result.ShouldBeNull();
        }
    }
}
