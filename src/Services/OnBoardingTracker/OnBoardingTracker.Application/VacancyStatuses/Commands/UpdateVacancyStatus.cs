using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.VacancyStatuses.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.VacancyStatuses.Commands
{
    public class UpdateVacancyStatus : IRequest<VacancyStatusModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public class Validator : AbstractValidator<UpdateVacancyStatus>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateVacancyStatus, VacancyStatusModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<VacancyStatusModel> Handle(UpdateVacancyStatus request, CancellationToken cancellationToken)
            {
                var vacancyStatus = await dbContext.VacancyStatuses
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (vacancyStatus == null)
                {
                    throw new NotFoundException(typeof(VacancyStatus).Name);
                }

                vacancyStatus.Name = request.Name;
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<VacancyStatusModel>(vacancyStatus);
            }
        }
    }
}
