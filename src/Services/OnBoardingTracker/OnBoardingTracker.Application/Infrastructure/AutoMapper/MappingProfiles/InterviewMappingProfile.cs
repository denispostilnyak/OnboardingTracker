using AutoMapper;
using OnBoardingTracker.Application.Interviews.Commands;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Application.Interviews.Queries;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Application.AutoMapper.MapperProfiles
{
    public class InterviewMappingProfile : Profile
    {
        public InterviewMappingProfile()
        {
            CreateMap<CreateInterview, Interview>();
            CreateMap<InterviewModel, Interview>();
            CreateMap<Interview, InterviewModel>();
        }
    }
}
