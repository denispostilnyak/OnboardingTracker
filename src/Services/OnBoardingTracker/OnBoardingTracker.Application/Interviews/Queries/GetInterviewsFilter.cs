using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Expressions.Extensions;
using OnBoardingTracker.Application.Infrastructure.Models;
using OnBoardingTracker.Application.Infrastructure.Paging.Extensions;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Interviews.Queries
{
    public class GetInterviewsFilter : IRequest<PaginatedResponse<InterviewModel>>
    {
        public string Title { get; set; }

        public int? CandidateId { get; set; }

        public int? VacancyId { get; set; }

        public DateTime? StartingTime { get; set; }

        public DateTime? EndingTime { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }

        public class Validator : AbstractValidator<GetInterviewsFilter>
        {
            public Validator()
            {
                RuleFor(x => x.Limit).NotEmpty();
                RuleFor(x => x.Page).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetInterviewsFilter, PaginatedResponse<InterviewModel>>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(IMapper mapper, OnboardingTrackerContext dbContext)
            {
                this.mapper = mapper;
                this.dbContext = dbContext;
            }

            public async Task<PaginatedResponse<InterviewModel>> Handle(GetInterviewsFilter request, CancellationToken cancellationToken)
            {
                var allInterviews = dbContext.Interviews.AsNoTracking();
                var filter = GetFilterExpression(request);

                var filteredInterviews = await allInterviews
                    .Where(filter)
                    .Paginate(request.Page, request.Limit)
                    .Select(x => mapper.Map<InterviewModel>(x)).
                    ToListAsync(cancellationToken);

                return new PaginatedResponse<InterviewModel>
                {
                    TotalCount = allInterviews.Count(),
                    Count = filteredInterviews.Count(),
                    Items = filteredInterviews,
                    Page = request.Page,
                    Limit = request.Limit
                };
            }

            private static Expression<Func<Interview, bool>> GetFilterExpression(GetInterviewsFilter filterModel)
            {
                Expression<Func<Interview, bool>> filter = x => true;
                if (filterModel.CandidateId.HasValue)
                {
                    filter = filter.AndAlso(x => x.CandidateId == filterModel.CandidateId);
                }

                if (filterModel.VacancyId.HasValue)
                {
                    filter = filter.AndAlso(x => x.VacancyId == filterModel.VacancyId);
                }

                if (filterModel.StartingTime.HasValue)
                {
                    filter = filter.AndAlso(x => x.StartingTime == filterModel.StartingTime);
                }

                if (filterModel.EndingTime.HasValue)
                {
                    filter = filter.AndAlso(x => x.EndingTime == filterModel.EndingTime);
                }

                if (filterModel.Title != null)
                {
                    filter = filter.AndAlso(x => x.Title == filterModel.Title);
                }

                return filter;
            }
        }
    }
}
