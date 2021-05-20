using AutoMapper;
using FluentValidation;
using MediatR;
using OnBoardingTracker.Application.Infrastructure.Exceptions;
using OnBoardingTracker.Application.Vacancy.Models;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Persistence;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Application.Vacancy.Comands
{
    public class UpdateVacancy : IRequest<VacancyModel>
    {
        public int Id { get; set; }

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

        public class Validator : AbstractValidator<UpdateVacancy>
        {
            public Validator()
            {
                RuleFor(s => s.Id).NotEmpty();
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

        public class Handler : IRequestHandler<UpdateVacancy, VacancyModel>
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

            public async Task<VacancyModel> Handle(UpdateVacancy request, CancellationToken cancellationToken)
            {
                var vacancy = await dbContext.Vacancies
                    .FindAsync(new object[] { request.Id }, cancellationToken);
                if (vacancy == null)
                {
                    throw new NotFoundException(typeof(Domain.Entities.Vacancy).Name);
                }

                var pictureUrl = request.FileStream != null ? await fileStorage.UploadFileAsync(request.FileStream, request.FileName, cancellationToken) : null;

                if (request.FileStream == null && request.FileName != string.Empty)
                {
                    vacancy.VacancyPictureUrl = request.FileName;
                }
                else
                {
                    vacancy.VacancyPictureUrl = pictureUrl?.ToString();
                }

                vacancy.Title = request.Title;
                vacancy.Description = request.Description;
                vacancy.SeniorityLevelId = request.SeniorityLevelId;
                vacancy.MaxSalary = request.MaxSalary;
                vacancy.JobTypeId = request.JobTypeId;
                vacancy.VacancyStatusId = request.VacancyStatusId;
                vacancy.WorkExperience = request.WorkExperience;
                vacancy.AssignedRecruiterId = request.AssignedRecruiterId;

                var vacancyModel = mapper.Map<VacancyModel>(vacancy);
                await dbContext.SaveChangesAsync(cancellationToken);

                return vacancyModel;
            }
        }
    }
}
