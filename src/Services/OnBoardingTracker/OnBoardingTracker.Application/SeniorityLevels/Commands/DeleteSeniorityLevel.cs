using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.SeniorityLevels.Commands
{
    public class DeleteSeniorityLevel : IRequest<SeniorityLevelModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteSeniorityLevel>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<DeleteSeniorityLevel, SeniorityLevelModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<SeniorityLevelModel> Handle(DeleteSeniorityLevel request, CancellationToken cancellationToken)
            {
                var seniorityLevel = await dbContext.SeniorityLevels
                    .FindAsync(new object[] { request.Id }, cancellationToken);

                if (seniorityLevel == null)
                {
                    throw new NotFoundException(typeof(SeniorityLevel).Name);
                }

                if (dbContext.Vacancies.Any(x => x.SeniorityLevelId == request.Id))
                {
                    throw new ValidationException($"Seniority Level with id: {request.Id} has relation with vacancies. Delete related vacancies first.");
                }

                dbContext.SeniorityLevels.Remove(seniorityLevel);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<SeniorityLevelModel>(seniorityLevel);
            }
        }
    }
}
