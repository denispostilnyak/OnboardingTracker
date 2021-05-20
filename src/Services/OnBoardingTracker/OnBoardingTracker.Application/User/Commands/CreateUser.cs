using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.User.Models;
using OnBoardingTracker.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.User.Commands
{
    public class CreateUser : IRequest<UserModel>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public class Validator : AbstractValidator<CreateUser>
        {
            public Validator()
            {
                RuleFor(user => user.Email).NotNull().MaximumLength(150);
            }
        }

        public class Handler : IRequestHandler<CreateUser, UserModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;

            public Handler(OnboardingTrackerContext context, IMapper mapper)
            {
                dbContext = context;
                this.mapper = mapper;
            }

            public async Task<UserModel> Handle(CreateUser request, CancellationToken cancellationToken)
            {
                var user = mapper.Map<Domain.Entities.User>(request);

                dbContext.Users.Add(user);

                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<UserModel>(user);
            }
        }
    }
}
