using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.Origins.Commands
{
    public class DeleteOrigin : IRequest<OriginModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteOrigin>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<DeleteOrigin, OriginModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<OriginModel> Handle(DeleteOrigin request, CancellationToken cancellationToken)
            {
                var origin = await dbContext.CandidateOrigins.FindAsync(new object[] { request.Id }, cancellationToken);
                if (origin == null)
                {
                    throw new NotFoundException(typeof(CandidateOrigin).Name);
                }

                if (dbContext.Candidates.Any(x => x.OriginId == request.Id))
                {
                    throw new ValidationException($"Candidate Origin with id: {request.Id} has relation with Candidates. Delete related candidates first.");
                }

                dbContext.CandidateOrigins.Remove(origin);
                await dbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<OriginModel>(origin);
            }
        }
    }
}
