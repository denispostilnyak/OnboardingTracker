using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingTracker.Application.Origins.Commands;
using OnBoardingTracker.Application.Origins.Models;
using OnBoardingTracker.Application.Origins.Queries;
using OnBoardingTracker.WebAPI.Controllers;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Controllers.v1.Origin
{
    [ApiVersion("1.0")]
    public class OriginsController : ApiController
    {
        public OriginsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(OriginList))]
        public async Task<ActionResult> GetOrigins()
        {
            return Ok(await Mediator.Send(new GetOrigins()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OriginModel))]
        public async Task<ActionResult> GetOriginById(int id)
        {
            return Ok(await Mediator.Send(new GetOriginById { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OriginModel))]
        public async Task<ActionResult> CreateOrigin([FromBody] CreateOrigin model)
        {
            var createdOrigin = await Mediator.Send(model);
            return CreatedAtAction(nameof(GetOriginById), new { id = createdOrigin }, createdOrigin);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OriginModel))]
        public async Task<ActionResult> UpdateOrigin([FromBody] UpdateOrigin model)
        {
            return Ok(await Mediator.Send(model));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OriginModel))]
        public async Task<ActionResult> DeleteOrigin(int id)
        {
            return Ok(await Mediator.Send(new DeleteOrigin { Id = id }));
        }
    }
}
