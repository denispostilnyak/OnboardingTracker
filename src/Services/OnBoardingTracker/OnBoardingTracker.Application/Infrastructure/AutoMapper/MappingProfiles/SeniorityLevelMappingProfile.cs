using AutoMapper;
using OnBoardingTracker.Application.SeniorityLevels.Commands;
using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Domain.Entities;
using System.Collections.Generic;

namespace OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles
{
    public class SeniorityLevelMappingProfile : Profile
    {
        public SeniorityLevelMappingProfile()
        {
            CreateMap<SeniorityLevel, SeniorityLevelModel>();
            CreateMap<SeniorityLevelModel, SeniorityLevel>();
            CreateMap<CreateSeniorityLevel, SeniorityLevel>();
        }
    }
}
