using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Expressions.Extensions;
using OnBoardingTracker.Application.Infrastructure.Models;
using OnBoardingTracker.Application.Infrastructure.Paging.Extensions;
using OnBoardingTracker.Application.Vacancy.Models;
using OnBoardingTracker.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Vacancy.Queries
{
    public class GetVacanciesFilter : IRequest<PaginatedResponse<VacancyModel>>
    {
        public int Page { get; set; }

        public int Limit { get; set; }

        public int? AssignedRecruiterId { get; set; }

        public int? SeniorityLevelId { get; set; }

        public int? VacancyStatusId { get; set; }

        public int? JobTypeId { get; set; }

        public class Validator : AbstractValidator<GetVacanciesFilter>
        {
            public Validator()
            {
                RuleFor(x => x.Limit).NotEmpty();
                RuleFor(x => x.Page).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetVacanciesFilter, PaginatedResponse<VacancyModel>>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public static Expression<Func<Domain.Entities.Vacancy, bool>> GetFilterExpression(GetVacanciesFilter filterModel)
            {
                Expression<Func<Domain.Entities.Vacancy, bool>> filter = x => true;
                if (filterModel.AssignedRecruiterId.HasValue)
                {
                    filter = filter.AndAlso(x => x.AssignedRecruiterId == filterModel.AssignedRecruiterId);
                }

                if (filterModel.JobTypeId.HasValue)
                {
                    filter = filter.AndAlso(x => x.JobTypeId == filterModel.JobTypeId);
                }

                if (filterModel.SeniorityLevelId.HasValue)
                {
                    filter = filter.AndAlso(x => x.SeniorityLevelId == filterModel.SeniorityLevelId);
                }

                if (filterModel.VacancyStatusId.HasValue)
                {
                    filter = filter.AndAlso(x => x.VacancyStatusId == filterModel.VacancyStatusId);
                }

                return filter;
            }

            public async Task<PaginatedResponse<VacancyModel>> Handle(GetVacanciesFilter request, CancellationToken cancellationToken)
            {
                var allVacancies = dbContext.Vacancies.AsNoTracking();
                var filter = GetFilterExpression(request);

                var filteredVacancies = await allVacancies
                    .Where(filter)
                    .Paginate(request.Page, request.Limit)
                    .Select(x => mapper.Map<VacancyModel>(x))
                    .ToListAsync(cancellationToken);

                var totalCount = allVacancies.Count();
                var count = filteredVacancies.Count;

                return new PaginatedResponse<VacancyModel>
                {
                    Items = filteredVacancies,
                    TotalCount = totalCount,
                    Count = count,
                    Page = request.Page,
                    Limit = request.Limit
                };
            }
        }
    }
}
