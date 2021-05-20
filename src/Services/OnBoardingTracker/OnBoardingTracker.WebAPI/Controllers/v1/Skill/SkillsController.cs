using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingTracker.Application.Skills.Commands;
using OnBoardingTracker.Application.Skills.Models;
using OnBoardingTracker.Application.Skills.Queries;
using OnBoardingTracker.WebAPI.Controllers;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Controllers.v1.Skill
{
    [ApiVersion("1.0")]
    public class SkillsController : ApiController
    {
        public SkillsController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SkillList))]
        public async Task<ActionResult> GetAllSkills()
        {
            return Ok(await Mediator.Send(new GetSkills()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SkillModel))]
        public async Task<ActionResult> GetSkillById(int id)
        {
            return Ok(await Mediator.Send(new GetSkillById { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SkillModel))]
        public async Task<ActionResult> CreateSkill([FromBody] CreateSkill model)
        {
            var createdSkill = await Mediator.Send(model);
            return CreatedAtAction(nameof(GetSkillById), new { id = createdSkill.Id }, createdSkill);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SkillModel))]
        public async Task<ActionResult> UpdateSkill([FromBody] UpdateSkill model)
        {
            return Ok(await Mediator.Send(model));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SkillModel))]
        public async Task<ActionResult> DeleteSkill(int id)
        {
            return Ok(await Mediator.Send(new DeleteSkill { Id = id }));
        }
    }
}
