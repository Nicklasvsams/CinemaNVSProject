using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.MovieServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<ActorResponse> actors = (List<ActorResponse>)await _actorService.GetAllActorsAsync();

                if (actors == null)
                {
                    return StatusCode(500);
                }

                if (actors.Count == 0)
                {
                    return NoContent();
                }

                return Ok(actors);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var actorResponse = await _actorService.GetActorByIdAsync(id);

                if (actorResponse == null)
                {
                    return NotFound();
                }

                return Ok(actorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ActorRequest actReq)
        {
            try
            {
                var actorResponse = await _actorService.CreateActorAsync(actReq);

                if (actorResponse == null)
                {
                    return StatusCode(500);
                }

                return Ok(actorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ActorRequest actReq)
        {
            try
            {
                var actorResponse = await _actorService.UpdateActorByIdAsync(id, actReq);

                if (actorResponse == null)
                {
                    return NotFound();
                }

                return Ok(actorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var actorResponse = await _actorService.DeleteActorByIdAsync(id);

                if (actorResponse == null)
                {
                    return NotFound();
                }

                return Ok(actorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
