using AutoMapper;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnBoardingTracker.Infrastructure.AutoMapper.Candidates
{
    class CandidateMappingProfile:Profile
    {
        public CandidateMappingProfile()
        {
            CreateMap<CreateCandidateModel, Candidate>();
            CreateMap<CandidateModel, Candidate>();
            CreateMap<Candidate, CandidateModel>();
        }
    }
}
