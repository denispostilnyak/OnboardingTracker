using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingTracker.Application.SeniorityLevels.Commands;
using OnBoardingTracker.Application.SeniorityLevels.Models;
using OnBoardingTracker.Application.SeniorityLevels.Queries;
using OnBoardingTracker.WebAPI.Controllers;

namespace OnBoardingTracker.WebApi.Controllers.v1.SeniorityLevel
{
    [ApiVersion("1.0")]
    public class SeniorityLevelsController : ApiController
    {
        public SeniorityLevelsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SeniorityLevelList))]
        public async Task<ActionResult> GetSeniorityLevels()
        {
            return Ok(await Mediator.Send(new GetSeniorityLevels()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SeniorityLevelModel))]
        public async Task<ActionResult> GetSeniorityLevelById(int id)
        {
            return Ok(await Mediator.Send(new GetSeniorityLevelById { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type=typeof(SeniorityLevelModel))]
        public async Task<ActionResult> CreateSeniorityLevel([FromBody] CreateSeniorityLevel model)
        {
            var createdSeniorityLevel = await Mediator.Send(model);
            return CreatedAtAction(nameof(GetSeniorityLevelById), new { id = createdSeniorityLevel.Id }, createdSeniorityLevel);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SeniorityLevelModel))]
        public async Task<ActionResult> UpdateSeniorityLevel([FromBody] UpdateSeniorityLevel model)
        {
            return Ok(await Mediator.Send(model));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SeniorityLevelModel))]
        public async Task<ActionResult> DeleteSeniorityLevel(int id)
        {
            return Ok(await Mediator.Send(new DeleteSeniorityLevel { Id = id }));
        }
    }
}
