using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.MovieServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CinemaNVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/<MovieController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<MovieResponse> movies = (List<MovieResponse>)await _movieService.GetAllMoviesAsync();

                if (movies == null)
                {
                    return StatusCode(500);
                }

                if (movies.Count == 0)
                {
                    return NoContent();
                }

                return Ok(movies);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var movieResponse = await _movieService.GetMovieByIdAsync(id);

                if (movieResponse == null)
                {
                    return NotFound();
                }

                return Ok(movieResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/<MovieController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody]MovieRequest movReq)
        {
            try
            {
                var movieResponse = await _movieService.CreateMovieAsync(movReq);

                if (movieResponse == null)
                {
                    return StatusCode(500);
                }

                return Ok(movieResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]MovieRequest movReq)
        {
            try
            {
                var movieResponse = await _movieService.UpdateMovieByIdAsync(movReq, id);

                if (movieResponse == null)
                {
                    return NotFound();
                }

                return Ok(movieResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                var movieResponse = await _movieService.DeleteMovieByIdAsync(id);

                if (movieResponse == null)
                {
                    return NotFound();
                }

                return Ok(movieResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
