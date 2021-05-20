using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Recruiters.Queries
{
    public class GetTopRecruiters : IRequest<RecruiterList>
    {
        public int Count { get; set; }

        public class Validator : AbstractValidator<GetTopRecruiters>
        {
            public Validator()
            {
                RuleFor(x => x.Count).NotEmpty();
                RuleFor(x => x.Count).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<GetTopRecruiters, RecruiterList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<RecruiterList> Handle(GetTopRecruiters request, CancellationToken cancellationToken)
            {
                var resultRecruiters = await dbContext.Recruiters
                    .Include(rc => rc.Vacancies)
                    .AsNoTracking()
                    .OrderByDescending(x => x.Vacancies.Count).Take(request.Count)
                    .Select(recruiter => mapper.Map<RecruiterModel>(recruiter))
                    .ToListAsync(cancellationToken);

                return new RecruiterList { Items = resultRecruiters };
            }
        }
    }
}
