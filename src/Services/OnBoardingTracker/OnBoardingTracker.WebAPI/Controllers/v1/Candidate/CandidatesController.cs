using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingTracker.Application.Candidates.Commands;
using OnBoardingTracker.Application.Candidates.Models;
using OnBoardingTracker.Application.Candidates.Queries;
using OnBoardingTracker.Application.Infrastructure.Models;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.WebApi.Controllers.v1.Models;
using OnBoardingTracker.WebAPI.Controllers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Controllers.v1.Candidate
{
    [ApiVersion("1.0")]
    public class CandidatesController : ApiController
    {
        public CandidatesController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CandidateModel))]
        public async Task<ActionResult> CreateCandidate([FromForm] CandidateViewModel model)
        {
            var applicationModel = new CreateCandidate
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                OriginId = model.OriginId,
                YearsOfExperience = model.YearsOfExperience,
                CurrentJobInformation = model.CurrentJobInformation
            };

            applicationModel.CvFileStream = model.CvFile.OpenReadStream();
            applicationModel.CvFileName = model.CvFile.FileName;

            applicationModel.ProfilePicFileStream = model.ProfilePicFile.OpenReadStream();
            applicationModel.ProfilePicFileName = model.ProfilePicFile.FileName;

            var createdCandidate = await Mediator.Send(applicationModel);
            return CreatedAtAction(nameof(GetCandidateById), new { id = createdCandidate.Id }, createdCandidate);
        }

        [HttpPost("{id}/cv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Uri))]
        public async Task<ActionResult> UploadCandidateCv(int id, IFormFile cvFile)
        {
            Stream cvStream = new MemoryStream();
            await cvFile.CopyToAsync(cvStream, CancellationToken.None);
            return Ok(await Mediator.Send(new UploadCandidateCv { Id = id, CvStream = cvStream, FileName = cvFile.FileName }));
        }

        [HttpGet("{id}/cv")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Uri))]
        public async Task<ActionResult> GetCandidateCvById(int id)
        {
            return Ok(await Mediator.Send(new GetCandidateCvById { Id = id }));
        }

        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<CandidateModel>))]
        public async Task<ActionResult> GetAllWithFilter([FromQuery] GetCandidatesFilter getCandidatesModel)
        {
            var result = await Mediator.Send(getCandidatesModel);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CandidateList))]
        public async Task<ActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetCandidates());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CandidateModel))]
        public async Task<ActionResult<CandidateModel>> GetCandidateById(int id)
        {
            var result = await Mediator.Send(new GetCandidateById { Id = id });
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CandidateModel))]
        public async Task<ActionResult> Update([FromForm] UpdateCandidateViewModel model)
        {
            var applicationModel = new UpdateCandidate
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                OriginId = model.OriginId,
                YearsOfExperience = model.YearsOfExperience,
                CurrentJobInformation = model.CurrentJobInformation
            };

            applicationModel.FileStream = model.CvFile.OpenReadStream();
            applicationModel.FileName = model.CvFile.FileName;

            applicationModel.ProfilePicFileStream = model.ProfilePicFile.OpenReadStream();
            applicationModel.ProfilePicFileName = model.ProfilePicFile.FileName;

            var result = await Mediator.Send(applicationModel);
            return Ok(result);
        }

        [HttpPut("assign/skills")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SkillList))]
        public async Task<ActionResult> AddSkills([FromBody] AssignCandidateSkills attachSkillsModel)
        {
            return Ok(await Mediator.Send(attachSkillsModel));
        }

        [HttpGet("{id}/skills")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SkillList))]
        public async Task<ActionResult> GetSkills(int id)
        {
            return Ok(await Mediator.Send(new GetCandidateSkills { Id = id }));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CandidateModel))]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteCandidate { Id = id });
            return Ok(result);
        }
    }
}