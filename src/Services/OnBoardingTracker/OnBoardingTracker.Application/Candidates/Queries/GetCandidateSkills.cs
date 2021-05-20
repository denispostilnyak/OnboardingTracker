using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Infrastructure.Models;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Candidates.Queries
{
    public class GetCandidateSkills : IRequest<ItemsCollection<SkillModel>>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetCandidateSkills>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<GetCandidateSkills, ItemsCollection<SkillModel>>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper)
            {
                this.mapper = mapper;
                this.dbContext = dbContext;
            }

            public Task<ItemsCollection<SkillModel>> Handle(GetCandidateSkills request, CancellationToken cancellationToken)
            {
                var skills = dbContext.CandidateSkills
                    .AsNoTracking()
                    .Where(x => x.CandidateId == request.Id);
                if (!skills.Any())
                {
                    throw new NotFoundException($"Candidate with an id of {request.Id}");
                }

                var mappedSkills = skills.Select(x => mapper.Map<SkillModel>(x.Skill));
                return Task.FromResult(new ItemsCollection<SkillModel> { Items = mappedSkills });
            }
        }
    }
}
