using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.MovieServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<DirectorResponse> actors = (List<DirectorResponse>)await _directorService.GetAllDirectorsAsync();

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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var directorResponse = await _directorService.GetDirectorByIdAsync(id);

                if (directorResponse == null)
                {
                    return NotFound();
                }

                return Ok(directorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DirectorRequest dirReq)
        {
            try
            {
                var directorResponse = await _directorService.CreateDirectorAsync(dirReq);

                if (directorResponse == null)
                {
                    return StatusCode(500);
                }

                return Ok(directorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DirectorRequest dirReq)
        {
            try
            {
                var directorResponse = await _directorService.UpdateDirectorByIdAsync(id, dirReq);

                if (directorResponse == null)
                {
                    return NotFound();
                }

                return Ok(directorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var directorResponse = await _directorService.DeleteDirectorByIdAsync(id);

                if (directorResponse == null)
                {
                    return NotFound();
                }

                return Ok(directorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
