using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Application.User.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.User.Queries
{
    public class GetUserByEmail : IRequest<UserModel>
    {
        public string Email { get; set; }

        public class Validator : AbstractValidator<GetUserByEmail>
        {
            public Validator()
            {
                RuleFor(item => item.Email).NotNull();
            }
        }

        public class Handler : IRequestHandler<GetUserByEmail, UserModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<UserModel> Handle(GetUserByEmail request, CancellationToken cancellationToken)
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == request.Email, cancellationToken);

                if (user == null)
                {
                    return null;
                }

                return mapper.Map<UserModel>(user);
            }
        }
    }
}
