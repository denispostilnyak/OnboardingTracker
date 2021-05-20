using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.SeniorityLevels.Queries
{
    public class GetSeniorityLevelById : IRequest<SeniorityLevelModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetSeniorityLevelById>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetSeniorityLevelById, SeniorityLevelModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<SeniorityLevelModel> Handle(GetSeniorityLevelById request, CancellationToken cancellationToken)
            {
                var seniorityLevel = await dbContext.SeniorityLevels
                    .FindAsync(request.Id);
                return mapper.Map<SeniorityLevelModel>(seniorityLevel);
            }
        }
    }
}
