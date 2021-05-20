using AutoMapper;
using OnBoardingTracker.Application.User.Commands;
using OnBoardingTracker.Application.User.Models;

namespace OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUser, Domain.Entities.User>();
            CreateMap<Domain.Entities.User, UserModel>();
            CreateMap<UserModel, Domain.Entities.User>();
        }
    }
}
