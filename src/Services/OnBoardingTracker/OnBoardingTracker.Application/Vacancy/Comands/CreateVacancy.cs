using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Vacancy.Models;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Persistence;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Vacancy.Comands
{
    public class CreateVacancy : IRequest<VacancyModel>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal MaxSalary { get; set; }

        public int AssignedRecruiterId { get; set; }

        public double WorkExperience { get; set; }

        public int SeniorityLevelId { get; set; }

        public int JobTypeId { get; set; }

        public int VacancyStatusId { get; set; }

        public string FileName { get; set; }

        public Stream FileStream { get; set; }

        public class Validator : AbstractValidator<CreateVacancy>
        {
            public Validator()
            {
                RuleFor(s => s.Title).NotNull().NotEmpty();
                RuleFor(s => s.Description).NotNull().NotEmpty();
                RuleFor(s => s.MaxSalary).GreaterThan(0);
                RuleFor(s => s.SeniorityLevelId).NotEmpty();
                RuleFor(s => s.AssignedRecruiterId).NotEmpty();
                RuleFor(s => s.VacancyStatusId).NotEmpty();
                RuleFor(s => s.JobTypeId).NotEmpty();
                RuleFor(s => s.WorkExperience).GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<CreateVacancy, VacancyModel>
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

            public async Task<VacancyModel> Handle(CreateVacancy request, CancellationToken cancellationToken)
            {
                var pictureUrl = request.FileStream != null ? await fileStorage.UploadFileAsync(request.FileStream, request.FileName, cancellationToken) : null;

                var vacancy = mapper.Map<Domain.Entities.Vacancy>(request);
                vacancy.VacancyPictureUrl = pictureUrl?.ToString();

                dbContext.Vacancies.Add(vacancy);
                await dbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<VacancyModel>(vacancy);
            }
        }
    }
}
