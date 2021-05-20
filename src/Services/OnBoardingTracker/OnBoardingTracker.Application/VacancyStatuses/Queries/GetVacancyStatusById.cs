using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.VacancyStatuses.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.VacancyStatuses.Queries
{
    public class GetVacancyStatusById : IRequest<VacancyStatusModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetVacancyStatusById>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetVacancyStatusById, VacancyStatusModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<VacancyStatusModel> Handle(GetVacancyStatusById request, CancellationToken cancellationToken)
            {
                var vacancyStatus = await dbContext.VacancyStatuses
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                return mapper.Map<VacancyStatusModel>(vacancyStatus);
            }
        }
    }
}
