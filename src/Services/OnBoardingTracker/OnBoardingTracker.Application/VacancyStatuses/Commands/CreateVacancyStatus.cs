using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.VacancyStatuses.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.VacancyStatuses.Commands
{
    public class CreateVacancyStatus : IRequest<VacancyStatusModel>
    {
        public string Name { get; set; }

        public class Validator : AbstractValidator<CreateVacancyStatus>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CreateVacancyStatus, VacancyStatusModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<VacancyStatusModel> Handle(CreateVacancyStatus request, CancellationToken cancellationToken)
            {
                var vacancyStatus = mapper.Map<VacancyStatus>(request);

                dbContext.VacancyStatuses.Add(vacancyStatus);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<VacancyStatusModel>(vacancyStatus);
            }
        }
    }
}
