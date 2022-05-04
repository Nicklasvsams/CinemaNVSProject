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
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieResponse>> GetAll()
        {
            return await _movieService.GetAllMovies();
        }
    }
}
