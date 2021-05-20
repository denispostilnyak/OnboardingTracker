using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Persistence;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Recruiters.Commands
{
    public class CreateRecruiter : IRequest<RecruiterModel>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string FileName { get; set; }

        public Stream FileStream { get; set; }

        public class Validator : AbstractValidator<CreateRecruiter>
        {
            public Validator()
            {
                RuleFor(recruiter => recruiter.FirstName).NotEmpty();
                RuleFor(recruiter => recruiter.LastName).NotEmpty();
                RuleFor(recruiter => recruiter.Email).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CreateRecruiter, RecruiterModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;
            private readonly IFileStorage fileStorage;

            public Handler(OnboardingTrackerContext context, IMapper mapper, IFileStorage fileStorage)
            {
                dbContext = context;
                this.mapper = mapper;
                this.fileStorage = fileStorage;
            }

            public async Task<RecruiterModel> Handle(CreateRecruiter request, CancellationToken cancellationToken)
            {
                var profilePicUrl = request.FileStream?.Length == 0 ? null :
                   await fileStorage.UploadFileAsync(request.FileStream, request.FileName, cancellationToken);
                var createdRecruiter = mapper.Map<Domain.Entities.Recruiter>(request);
                createdRecruiter.PictureUrl = profilePicUrl;

                dbContext.Recruiters.Add(createdRecruiter);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<RecruiterModel>(createdRecruiter);
            }
        }
    }
}
