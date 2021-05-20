using AutoMapper;
using OnBoardingTracker.Application.Recruiters.Commands;
using OnBoardingTracker.Application.Recruiters.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles
{
    public class RecruiterMappingProfile : Profile
    {
        public RecruiterMappingProfile()
        {
            CreateMap<CreateRecruiter, Domain.Entities.Recruiter>();
            CreateMap<RecruiterModel, Domain.Entities.Recruiter>();
            CreateMap<Domain.Entities.Recruiter, RecruiterModel>();
        }
    }
}
