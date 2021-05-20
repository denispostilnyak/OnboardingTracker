using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Infrastructure.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = OnBoardingTracker.Application.Infrastructure.Exceptions.ValidationException;

namespace OnBoardingTracker.Application.Vacancy.Comands
{
    public class AssignCandidatesToVacancy
        : IRequest<ItemsCollection<CandidateModel>>
    {
        public int VacancyId { get; set; }

        public IEnumerable<int> CandidatesIdList { get; set; }

        public class Validator
            : AbstractValidator<AssignCandidatesToVacancy>
        {
            public Validator()
            {
                RuleFor(item => item.VacancyId).NotEmpty();
                RuleFor(item => item.CandidatesIdList).NotEmpty();
            }
        }

        public class Handler
            : IRequestHandler<AssignCandidatesToVacancy, ItemsCollection<CandidateModel>>
        {
            private readonly OnboardingTrackerContext dbContext;

            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<ItemsCollection<CandidateModel>> Handle(AssignCandidatesToVacancy request, CancellationToken cancellationToken)
            {
                var candidatesIdList = request.CandidatesIdList;
                var vacancyId = request.VacancyId;
                var candidatesList = new List<CandidateModel>();

                var vacancy = await dbContext.Vacancies
                    .FindAsync(new object[] { vacancyId }, cancellationToken);
                if (vacancy == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.Vacancy).Name);
                }

                var candidates = await dbContext.Candidates.ToListAsync(cancellationToken);
                var vacancyCandidates = await dbContext.CandidateVacancies
                    .Where(x => x.VacancyId == request.VacancyId)
                    .ToListAsync(cancellationToken);

                foreach (var candidateId in candidatesIdList)
                {
                    if (!candidates.Exists(x => x.Id == candidateId))
                    {
                        throw new NotFoundException($"candidate with id: {candidateId}");
                    }

                    if (vacancyCandidates.Exists(x => x.CandidateId == candidateId))
                    {
                        throw new ValidationException($"Candidate with id: {candidateId} is already exist on this vacancy");
                    }

                    dbContext.CandidateVacancies.Add(
                       new CandidateVacancy
                       {
                           CandidateId = candidateId,
                           VacancyId = vacancyId
                       });

                    candidatesList.Add(mapper.Map<CandidateModel>(candidates.FirstOrDefault(candidate => candidate.Id == candidateId)));
                }

                await dbContext.SaveChangesAsync();
                return new ItemsCollection<CandidateModel> { Items = candidatesList };
            }
        }
    }
}
