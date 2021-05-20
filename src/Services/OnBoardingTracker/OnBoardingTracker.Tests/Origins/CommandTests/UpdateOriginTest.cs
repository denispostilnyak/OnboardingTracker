using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Origins.Commands;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Application.Tests.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnBoardingTracker.Application.Tests.Origins.CommandTests
{
    public class UpdateOriginTest : BaseTest
    {
        [Fact]
        public void Validate_Positive()
        {
            var validator = new UpdateOrigin.Validator();
            validator.Validate(new UpdateOrigin { Id = 1, Name = "NotEmpty" }).IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_Negative()
        {
            var validator = new UpdateOrigin.Validator();
            validator.Validate(new UpdateOrigin()).Errors.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Handle_Positive()
        {
            var handler = new UpdateOrigin.Handler(dbContext, AutoMapperHelper.Mapper);
            var entity = dbContext.CandidateOrigins.First(x => x.Id == 1);
            var result = await handler.Handle(new UpdateOrigin { Id = 1, Name = "Updated" }, CancellationToken.None);

            result.ShouldBeOfType<OriginModel>();
            result.Name.ShouldBe("Updated");
            entity.Name.ShouldBe("Updated");
        }

        [Fact]
        public async Task Handle_Negative_InvalidId_ThrowsNotFoundException()
        {
            var handler = new UpdateOrigin.Handler(dbContext, AutoMapperHelper.Mapper);
            var result = handler.Handle(new UpdateOrigin { Id = -1, Name = "Updated" }, CancellationToken.None);
            await result.ShouldThrowAsync<NotFoundException>();
        }
    }
}
