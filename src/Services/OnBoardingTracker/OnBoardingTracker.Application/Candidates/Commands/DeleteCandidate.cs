using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.Candidates.Commands
{
    public class DeleteCandidate : IRequest<CandidateModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteCandidate>
        {
            public Validator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<DeleteCandidate, CandidateModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<CandidateModel> Handle(DeleteCandidate request, CancellationToken cancellationToken)
            {
                var candidate = await dbContext.Candidates
                    .Include(x => x.Interviews)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (candidate == null)
                {
                    throw new NotFoundException(typeof(Candidate).Name);
                }

                if (candidate.Interviews.Any())
                {
                    throw new ValidationException($"Candidate with id: {request.Id} has relation with Interviews.Delete related Interviews first.");
                }

                dbContext.Candidates.Remove(candidate);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<CandidateModel>(candidate);
            }
        }
    }
}
