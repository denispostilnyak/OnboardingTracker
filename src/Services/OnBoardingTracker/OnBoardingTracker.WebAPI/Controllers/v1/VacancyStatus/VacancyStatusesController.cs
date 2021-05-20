using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingTracker.Application.VacancyStatuses.Commands;
using OnBoardingTracker.Application.VacancyStatuses.Models;
using OnBoardingTracker.Application.VacancyStatuses.Queries;
using OnBoardingTracker.WebAPI.Controllers;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Controllers.v1.VacancyStatus
{
    [ApiVersion("1.0")]
    public class VacancyStatusesController : ApiController
    {
        public VacancyStatusesController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyStatusList))]
        public async Task<ActionResult> GetVacancyStatuses()
        {
            return Ok(await Mediator.Send(new GetVacancyStatuses()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyStatusModel))]
        public async Task<ActionResult> GetVacancyStatusById(int id)
        {
            return Ok(await Mediator.Send(new GetVacancyStatusById { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VacancyStatusModel))]
        public async Task<ActionResult> CreateVacancyStatus([FromBody] CreateVacancyStatus model)
        {
            var createdVacancyStatus = await Mediator.Send(model);
            return CreatedAtAction(nameof(GetVacancyStatusById), new { id = createdVacancyStatus.Id }, createdVacancyStatus);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyStatusModel))]
        public async Task<ActionResult> UpdateVacancyStatus([FromBody] UpdateVacancyStatus model)
        {
            return Ok(await Mediator.Send(model));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VacancyStatusModel))]
        public async Task<ActionResult> DeleteVacancyStatus(int id)
        {
            return Ok(await Mediator.Send(new DeleteVacancyStatus { Id = id }));
        }
    }
}
