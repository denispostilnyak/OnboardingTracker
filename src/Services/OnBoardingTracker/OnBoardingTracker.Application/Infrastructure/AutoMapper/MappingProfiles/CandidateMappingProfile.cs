using AutoMapper;
using OnBoardingTracker.Application.Candidates.Commands;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Candidates.Queries;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles
{
    public class CandidateMappingProfile : Profile
    {
        public CandidateMappingProfile()
        {
            CreateMap<CandidateModel, Candidate>();
            CreateMap<CreateCandidate, Candidate>();
            CreateMap<Candidate, CandidateModel>();
        }
    }
}
