using AutoMapper;
using OnBoardingTracker.Application.JobTypes.Commands;
using OnBoardingTracker.Application.JobTypes.Models;
using System.Collections.Generic;

namespace OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles
{
    public class JobTypeMappingProfile : Profile
    {
        public JobTypeMappingProfile()
        {
            CreateMap<CreateJobType, Domain.Entities.JobType>();
            CreateMap<JobTypeModel, Domain.Entities.JobType>();
            CreateMap<Domain.Entities.JobType, JobTypeModel>();
        }
    }
}
