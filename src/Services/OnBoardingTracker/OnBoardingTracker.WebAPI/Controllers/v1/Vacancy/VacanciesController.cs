using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Infrastructure.Models;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Application.Vacancy.Comands;
using OnBoardingTracker.Application.Vacancy.Models;
using OnBoardingTracker.Application.Vacancy.Queries;
using OnBoardingTracker.WebApi.Controllers.v1.Models;
using OnBoardingTracker.WebAPI.Controllers;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Controllers.v1.Vacancy
{
    [ApiVersion("1.0")]
    public class VacanciesController : ApiController
    {
        public VacanciesController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyList))]
        public async Task<ActionResult> GetAllVacancies()
        {
            return Ok(await Mediator.Send(new GetVacancies()));
        }

        [AllowAnonymous]
        [HttpGet("top")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyList))]
        public async Task<ActionResult> GetTopVacancies()
        {
            return Ok(await Mediator.Send(new GetTopVacancies()));
        }

        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyList))]
        public async Task<ActionResult> GetVacanciesFilter([FromQuery] GetVacanciesFilter vacanciesFilter)
        {
            return Ok(await Mediator.Send(vacanciesFilter));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyModel))]
        public async Task<ActionResult> GetVacancyById(int id)
        {
            return Ok(await Mediator.Send(new GetVacancyById { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VacancyModel))]
        public async Task<ActionResult> CreateVacancy([FromForm] VacancyViewModel createVacancyModel)
        {
            var applicationModel = new CreateVacancy
            {
                Title = createVacancyModel.Title,
                Description = createVacancyModel.Description,
                MaxSalary = createVacancyModel.MaxSalary,
                WorkExperience = createVacancyModel.WorkExperience,
                AssignedRecruiterId = createVacancyModel.AssignedRecruiterId,
                SeniorityLevelId = createVacancyModel.SeniorityLevelId,
                JobTypeId = createVacancyModel.JobTypeId,
                VacancyStatusId = createVacancyModel.VacancyStatusId
            };

            if (createVacancyModel.VacancyPicture.Length != 0)
            {
                applicationModel.FileStream = createVacancyModel.VacancyPicture.OpenReadStream();
                applicationModel.FileName = createVacancyModel.VacancyPicture.FileName;
            }

            var createdVacancy = await Mediator.Send(applicationModel);
            return CreatedAtAction(nameof(GetVacancyById), new { id = createdVacancy.Id }, createdVacancy);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyModel))]
        public async Task<ActionResult> UpdateVacancy([FromForm] UpdateVacancyViewModel vacancyModel)
        {
            var applicationModel = new UpdateVacancy
            {
                Id = vacancyModel.Id,
                Description = vacancyModel.Description,
                Title = vacancyModel.Title,
                MaxSalary = vacancyModel.MaxSalary,
                WorkExperience = vacancyModel.WorkExperience,
                AssignedRecruiterId = vacancyModel.AssignedRecruiterId,
                SeniorityLevelId = vacancyModel.SeniorityLevelId,
                JobTypeId = vacancyModel.JobTypeId,
                VacancyStatusId = vacancyModel.VacancyStatusId
            };

            if (vacancyModel.VacancyPicture?.Length != 0)
            {
                applicationModel.FileStream = vacancyModel.VacancyPicture.OpenReadStream();
                applicationModel.FileName = vacancyModel.VacancyPicture.FileName;
            }

            if (!string.IsNullOrEmpty(vacancyModel.VacancyPicture.FileName))
            {
                applicationModel.FileName = vacancyModel.VacancyPicture.FileName;
            }

            return Ok(await Mediator.Send(applicationModel));
        }

        [HttpPut("assign/skills")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SkillList))]
        public async Task<ActionResult> AddSkillsToVacancy([FromBody] AssignSkillsToVacancy assignSkillsToVacancy)
        {
            return Ok(await Mediator.Send(assignSkillsToVacancy));
        }

        [HttpPut("assign/candidates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemsCollection<CandidateModel>))]
        public async Task<ActionResult> AddCandidatesToVacancy([FromBody] AssignCandidatesToVacancy assignCandidatesToVacancy)
        {
            return Ok(await Mediator.Send(assignCandidatesToVacancy));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyModel))]
        public async Task<ActionResult> DeleteVacancy(int id)
        {
            return Ok(await Mediator.Send(new DeleteVacancy { Id = id }));
        }
    }
}
