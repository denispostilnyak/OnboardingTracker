using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.Recruiters.Commands
{
    public class DeleteRecruiter : IRequest<RecruiterModel>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteRecruiter>
        {
            public Validator()
            {
                RuleFor(recruiter => recruiter.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<DeleteRecruiter, RecruiterModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<RecruiterModel> Handle(DeleteRecruiter request, CancellationToken cancellationToken)
            {
                var deletedRecruiter = await dbContext.Recruiters
                    .Include(x => x.Vacancies)
                    .Include(x => x.Interviews)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (deletedRecruiter == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.Recruiter).Name);
                }

                if (deletedRecruiter.Vacancies.Any() || deletedRecruiter.Interviews.Any())
                {
                    throw new ValidationException($"Recruiter with id: {request.Id} has relation with Vacancies.Delete them first");
                }

                dbContext.Recruiters.Remove(deletedRecruiter);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<RecruiterModel>(deletedRecruiter);
            }
        }
    }
}
