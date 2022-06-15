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
    public class MovieActorController : ControllerBase
    {
        private readonly IMovieActorService _movieActorService;

        public MovieActorController(IMovieActorService movieActorService)
        {
            _movieActorService = movieActorService;
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
                List<MovieActorResponse> actors = (List<MovieActorResponse>)await _movieActorService.GetAllMovieActorsAsync();

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

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MovieActorRequest movActReq)
        {
            try
            {
                var movieActorResponse = await _movieActorService.CreateMovieActorAsync(movActReq);

                if (movieActorResponse == null)
                {
                    return StatusCode(500);
                }

                return Ok(movieActorResponse);
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
                var movieActorResponse = await _movieActorService.DeleteMovieActorAsync(id);

                if (movieActorResponse == null)
                {
                    return NotFound();
                }

                return Ok(movieActorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
