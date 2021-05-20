using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingTracker.Application.Infrastructure.Models;
using OnBoardingTracker.Application.Interviews.Commands;
using OnBoardingTracker.Application.Interviews.Models;
using OnBoardingTracker.Application.Interviews.Queries;
using OnBoardingTracker.WebAPI.Controllers;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Controllers.v1.Interview
{
    [ApiVersion("1.0")]
    public class InterviewsController : ApiController
    {
        public InterviewsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InterviewList))]
        public async Task<ActionResult> GetInterviews()
        {
            var result = await Mediator.Send(new GetInterviews());
            return Ok(result);
        }

        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<InterviewModel>))]
        public async Task<ActionResult> GetInterviewsFilter([FromQuery] GetInterviewsFilter interviewsFilter)
        {
            return Ok(await Mediator.Send(interviewsFilter));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteInterview() { Id = id });
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InterviewModel))]
        public async Task<ActionResult> GetInterviewById(int id)
        {
            var result = await Mediator.Send(new GetInterviewById() { Id = id });
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InterviewModel))]
        public async Task<ActionResult> Update([FromBody] UpdateInterview model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("assign/vacancy")]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(InterviewModel))]
        public async Task<ActionResult> AssignToVacancy([FromBody] AssignInterviewToVacancy model)
        {
            return Ok(await Mediator.Send(model));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InterviewModel))]
        public async Task<ActionResult> Create([FromBody] CreateInterview model)
        {
            var createdInterview = await Mediator.Send(model);
            return CreatedAtAction(nameof(GetInterviewById), new { id = createdInterview.Id }, createdInterview);
        }
    }
}
