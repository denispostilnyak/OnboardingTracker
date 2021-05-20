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
    public class DeleteVacancyStatus : IRequest<VacancyStatusModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteVacancyStatus>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<DeleteVacancyStatus, VacancyStatusModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<VacancyStatusModel> Handle(DeleteVacancyStatus request, CancellationToken cancellationToken)
            {
                var vacancyStatus = await dbContext.VacancyStatuses
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (vacancyStatus == null)
                {
                    throw new NotFoundException(typeof(VacancyStatus).Name);
                }

                dbContext.VacancyStatuses.Remove(vacancyStatus);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<VacancyStatusModel>(vacancyStatus);
            }
        }
    }
}
