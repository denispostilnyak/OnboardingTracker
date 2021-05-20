using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Interviews.Queries
{
    public class GetInterviews : IRequest<InterviewList>
    {
        public class Validator : AbstractValidator<GetInterviews>
        {
            public Validator()
            {
            }
        }

        public class Handler : IRequestHandler<GetInterviews, InterviewList>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<InterviewList> Handle(GetInterviews request, CancellationToken cancellationToken)
            {
                var allInterviews = dbContext.Interviews.AsNoTracking();

                var interviewModels = await allInterviews.
                    Select(x => mapper.Map<InterviewModel>(x)).
                    ToListAsync(cancellationToken);

                return new InterviewList { Items = interviewModels };
            }
        }
    }
}
