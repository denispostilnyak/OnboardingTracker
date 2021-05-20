using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Persistence;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Candidates.Commands
{
    public class CreateCandidate : IRequest<CandidateModel>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int OriginId { get; set; }

        public double YearsOfExperience { get; set; }

        public string CurrentJobInformation { get; set; }

        public string CvFileName { get; set; }

        public Stream CvFileStream { get; set; }

        public string ProfilePicFileName { get; set; }

        public Stream ProfilePicFileStream { get; set; }

        public class Validator : AbstractValidator<CreateCandidate>
        {
            public Validator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.PhoneNumber).NotEmpty();
                RuleFor(x => x.Email).EmailAddress();
                RuleFor(x => x.OriginId).NotEmpty();
                RuleFor(x => x.YearsOfExperience).GreaterThanOrEqualTo(0);
            }
        }

        public class Handler : IRequestHandler<CreateCandidate, CandidateModel>
        {
            private readonly OnboardingTrackerContext dbContext;
            private readonly IMapper mapper;
            private readonly IFileStorage fileStorage;

            public Handler(OnboardingTrackerContext dbContext, IMapper mapper, IFileStorage fileStorage)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
                this.fileStorage = fileStorage;
            }

            public async Task<CandidateModel> Handle(CreateCandidate request, CancellationToken cancellationToken)
            {
                var cvUrl = request.CvFileStream?.Length == 0 ? null :
                    await fileStorage.UploadFileAsync(request.CvFileStream, request.CvFileName, cancellationToken);
                var profilePicUrl = request.ProfilePicFileStream?.Length == 0 ? null :
                    await fileStorage.UploadFileAsync(request.ProfilePicFileStream, request.ProfilePicFileName, cancellationToken);
                var candidate = mapper.Map<Candidate>(request);

                candidate.CvUrl = cvUrl;
                candidate.ProfilePicture = profilePicUrl;

                dbContext.Candidates.Add(candidate);
                await dbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<CandidateModel>(candidate);
            }
        }
    }
}
