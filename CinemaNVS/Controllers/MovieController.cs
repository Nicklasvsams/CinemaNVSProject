using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.MovieServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public async Task<MovieResponse> Get(int id)
        {
            return await _movieService.GetMovieByIdAsync(id);
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task<MovieResponse> Post([FromBody] MovieRequest movReq)
        {
            return await _movieService.CreateMovieAsync(movReq);
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<MovieResponse> Put(int id, [FromBody] MovieRequest movReq)
        {
            return await _movieService.UpdateMovieByIdAsync(movReq, id);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public async Task<MovieResponse> Delete(int id)
        {
            return await _movieService.DeleteMovieByIdAsync(id);
        }
    }
}
