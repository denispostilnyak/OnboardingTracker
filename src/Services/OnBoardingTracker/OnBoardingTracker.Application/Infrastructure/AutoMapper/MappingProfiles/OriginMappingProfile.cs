using AutoMapper;
using OnBoardingTracker.Application.Origins.Commands;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles
{
    public class OriginMappingProfile : Profile
    {
        public OriginMappingProfile()
        {
            CreateMap<CandidateOrigin, OriginModel>();
            CreateMap<OriginModel, CandidateOrigin>();
            CreateMap<CreateOrigin, CandidateOrigin>();
        }
    }
}
