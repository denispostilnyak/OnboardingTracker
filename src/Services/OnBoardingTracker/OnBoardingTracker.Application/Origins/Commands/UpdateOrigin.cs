using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Origins.Commands
{
    public class UpdateOrigin : IRequest<OriginModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public class Validator : AbstractValidator<UpdateOrigin>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateOrigin, OriginModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<OriginModel> Handle(UpdateOrigin request, CancellationToken cancellationToken)
            {
                var origin = await dbContext.CandidateOrigins.FindAsync(new object[] { request.Id }, cancellationToken);
                if (origin == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.CandidateOrigin).Name);
                }

                origin.Name = request.Name;
                await dbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<OriginModel>(origin);
            }
        }
    }
}
