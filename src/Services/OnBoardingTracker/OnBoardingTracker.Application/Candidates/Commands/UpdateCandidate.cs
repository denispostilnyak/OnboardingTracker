using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Persistence;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Candidates.Commands
{
    public class UpdateCandidate : IRequest<CandidateModel>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int OriginId { get; set; }

        public double YearsOfExperience { get; set; }

        public string CurrentJobInformation { get; set; }

        public string FileName { get; set; }

        public Stream FileStream { get; set; }

        public string ProfilePicFileName { get; set; }

        public Stream ProfilePicFileStream { get; set; }

        public class Validator : AbstractValidator<UpdateCandidate>
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

        public class Handler : IRequestHandler<UpdateCandidate, CandidateModel>
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

            public async Task<CandidateModel> Handle(UpdateCandidate request, CancellationToken cancellationToken)
            {
                var cvUrl = request.FileStream?.Length == 0 ? null :
                    await fileStorage.UploadFileAsync(request.FileStream, request.FileName, cancellationToken);
                var profilePicUrl = request.ProfilePicFileStream?.Length == 0 ? null :
                    await fileStorage.UploadFileAsync(request.ProfilePicFileStream, request.ProfilePicFileName, cancellationToken);

                var candidate = await dbContext.Candidates.FindAsync(new object[] { request.Id }, cancellationToken);
                if (candidate == null)
                {
                    throw new NotFoundException(typeof(Candidate).Name);
                }

                candidate.FirstName = request.FirstName;
                candidate.LastName = request.LastName;
                candidate.OriginId = request.OriginId;
                candidate.PhoneNumber = request.PhoneNumber;
                candidate.YearsOfExperience = request.YearsOfExperience;
                candidate.Email = request.Email;
                candidate.CurrentJobInformation = request.CurrentJobInformation;
                if (cvUrl != null)
                {
                    candidate.CvUrl = cvUrl;
                }

                if (profilePicUrl != null)
                {
                    candidate.ProfilePicture = profilePicUrl;
                }

                await dbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<CandidateModel>(candidate);
            }
        }
    }
}
