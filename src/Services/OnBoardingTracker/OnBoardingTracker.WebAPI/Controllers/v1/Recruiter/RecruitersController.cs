using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingTracker.Application.Recruiters.Commands;
using OnBoardingTracker.Application.Recruiters.Models;
using OnBoardingTracker.Application.Recruiters.Queries;
using OnBoardingTracker.WebApi.Controllers.v1.Models;
using OnBoardingTracker.WebApi.Controllers.v1.Models.Recruiters;
using OnBoardingTracker.WebAPI.Controllers;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Controllers.v1.Recruiter
{
    [ApiVersion("1.0")]
    public class RecruitersController : ApiController
    {
        public RecruitersController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecruiterList))]
        public async Task<ActionResult> GetAllRecruiters()
        {
            return Ok(await Mediator.Send(new GetRecruiters()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecruiterModel))]
        public async Task<ActionResult> GetRecruiterById(int id)
        {
            return Ok(await Mediator.Send(new GetRecruiterById { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RecruiterModel))]
        public async Task<ActionResult> CreateRecruiter([FromForm] CreateRecruiterViewModel recruiterModel)
        {
            var createRecruiterRequest = new CreateRecruiter
            {
                FirstName = recruiterModel.FirstName,
                LastName = recruiterModel.LastName,
                Email = recruiterModel.Email,
                FileName = recruiterModel.Picture.FileName,
                FileStream = recruiterModel.Picture.OpenReadStream()
            };
            var createdRecruiter = await Mediator.Send(createRecruiterRequest);
            return CreatedAtAction(nameof(GetRecruiterById), new { id = createdRecruiter.Id }, createdRecruiter);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecruiterModel))]
        public async Task<ActionResult> UpdateRecruiter([FromForm] UpdateRecruiterViewModel recruiterModel)
        {
            var updateRecruiterRequest = new UpdateRecruiter
            {
                Id = recruiterModel.Id,
                FirstName = recruiterModel.FirstName,
                LastName = recruiterModel.LastName,
                Email = recruiterModel.Email,
                FileName = recruiterModel.Picture.FileName,
                FileStream = recruiterModel.Picture.OpenReadStream()
            };
            var result = await Mediator.Send(updateRecruiterRequest);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecruiterModel))]
        public async Task<ActionResult> DeleteRecruiter(int id)
        {
            return Ok(await Mediator.Send(new DeleteRecruiter { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet("top")]
        public async Task<ActionResult> GetTopRecruiters([FromQuery] int count)
        {
            return Ok(await Mediator.Send(new GetTopRecruiters { Count = count }));
        }
    }
}
