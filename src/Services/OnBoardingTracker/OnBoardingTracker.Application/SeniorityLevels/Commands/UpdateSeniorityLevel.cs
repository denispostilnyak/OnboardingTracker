using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.SeniorityLevels.Commands
{
    public class UpdateSeniorityLevel : IRequest<SeniorityLevelModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public class Validator : AbstractValidator<UpdateSeniorityLevel>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateSeniorityLevel, SeniorityLevelModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<SeniorityLevelModel> Handle(UpdateSeniorityLevel request, CancellationToken cancellationToken)
            {
                var seniorityLevel = await dbContext.SeniorityLevels
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (seniorityLevel == null)
                {
                    throw new NotFoundException(typeof(SeniorityLevel).Name);
                }

                seniorityLevel.Name = request.Name;
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<SeniorityLevelModel>(seniorityLevel);
            }
        }
    }
}
