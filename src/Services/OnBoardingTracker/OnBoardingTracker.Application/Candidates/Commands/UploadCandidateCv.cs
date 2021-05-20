using FluentValidation;
using MediatR;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Persistence;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Candidates.Commands
{
    public class UploadCandidateCv : IRequest<Uri>
    {
        public int Id { get; set; }

        public Stream CvStream { get; set; }

        public string FileName { get; set; }

        public class Validator : AbstractValidator<UploadCandidateCv>
        {
            public Validator()
            {
                RuleFor(x => x.CvStream).Must(stream => stream.Length > 0);
                RuleFor(x => x.FileName).NotEmpty();
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UploadCandidateCv, Uri>
        {
            private readonly IFileStorage fileStorage;

            private readonly OnboardingTrackerContext dbContext;

            public Handler(IFileStorage fileStorage, OnboardingTrackerContext dbContext)
            {
                this.fileStorage = fileStorage;
                this.dbContext = dbContext;
            }

            public async Task<Uri> Handle(UploadCandidateCv request, CancellationToken cancellationToken)
            {
                var uri = await fileStorage.UploadFileAsync(request.CvStream, request.FileName, cancellationToken);
                var candidate = await dbContext.Candidates.FindAsync(new object[] { request.Id }, cancellationToken);
                candidate.CvUrl = uri;
                await dbContext.SaveChangesAsync();
                return uri;
            }
        }
    }
}
