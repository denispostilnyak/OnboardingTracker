using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Infrastructure.Expressions.Extensions;
using OnBoardingTracker.Application.Infrastructure.Models;
using OnBoardingTracker.Application.Infrastructure.Paging.Extensions;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Candidates.Queries
{
    public class GetCandidatesFilter : IRequest<PaginatedResponse<CandidateModel>>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int? OriginId { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }

        public class Validator : AbstractValidator<GetCandidatesFilter>
        {
            public Validator()
            {
                RuleFor(x => x.Limit).NotEmpty();
                RuleFor(x => x.Page).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetCandidatesFilter, PaginatedResponse<CandidateModel>>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(IMapper mapper, OnboardingTrackerContext dbContext)
            {
                this.mapper = mapper;
                this.dbContext = dbContext;
            }

            public async Task<PaginatedResponse<CandidateModel>> Handle(GetCandidatesFilter request, CancellationToken cancellationToken)
            {
                var allCandidates = dbContext.Candidates.AsNoTracking();
                var filter = GetFilterExpression(request);

                var filteredCandidates = await allCandidates
                    .Where(filter)
                    .Paginate(request.Page, request.Limit)
                    .Select(x => mapper.Map<CandidateModel>(x))
                    .ToListAsync(cancellationToken);

                var count = filteredCandidates.Count();
                var totalCount = allCandidates.Count();

                return new PaginatedResponse<CandidateModel>
                {
                    Items = filteredCandidates,
                    Count = count,
                    TotalCount = totalCount,
                    Page = request.Page,
                    Limit = request.Limit
                };
            }

            private static Expression<Func<Candidate, bool>> GetFilterExpression(GetCandidatesFilter filterModel)
            {
                Expression<Func<Candidate, bool>> filter = x => true;
                if (!string.IsNullOrEmpty(filterModel.Email))
                {
                    filter = filter.AndAlso(x => x.Email == filterModel.Email);
                }

                if (!string.IsNullOrEmpty(filterModel.FirstName))
                {
                    filter = filter.AndAlso(x => x.FirstName == filterModel.FirstName);
                }

                if (!string.IsNullOrEmpty(filterModel.LastName))
                {
                    filter = filter.AndAlso(x => x.LastName == filterModel.LastName);
                }

                if (filterModel.OriginId.HasValue)
                {
                    filter = filter.AndAlso(x => x.OriginId == filterModel.OriginId);
                }

                if (!string.IsNullOrEmpty(filterModel.PhoneNumber))
                {
                    filter = filter.AndAlso(x => x.PhoneNumber == filterModel.PhoneNumber);
                }

                return filter;
            }
        }
    }
}
