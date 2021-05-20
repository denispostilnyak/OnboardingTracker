using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Persistence;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Recruiters.Commands
{
    public class UpdateRecruiter : IRequest<RecruiterModel>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string FileName { get; set; }

        public Stream FileStream { get; set; }

        public class Validator : AbstractValidator<UpdateRecruiter>
        {
            public Validator()
            {
                RuleFor(item => item.Id).NotEmpty();
                RuleFor(item => item.FirstName).NotEmpty();
                RuleFor(item => item.LastName).NotEmpty();
                RuleFor(item => item.Email).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateRecruiter, RecruiterModel>
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

            public async Task<RecruiterModel> Handle(UpdateRecruiter request, CancellationToken cancellationToken)
            {
                var updatedRecruiter = await dbContext.Recruiters.
                    FindAsync(new object[] { request.Id }, cancellationToken);

                if (updatedRecruiter == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.Recruiter).Name);
                }

                updatedRecruiter.FirstName = request.FirstName;
                updatedRecruiter.LastName = request.LastName;
                updatedRecruiter.Email = request.Email;

                if (request.FileStream != null && request.FileStream?.Length != 0)
                {
                    var pictureUrl = request.FileStream != null ? await fileStorage.UploadFileAsync(request.FileStream, request.FileName, cancellationToken) : null;
                    updatedRecruiter.PictureUrl = new Uri(pictureUrl?.ToString());
                }

                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<RecruiterModel>(updatedRecruiter);
            }
        }
    }
}
