using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.TransactionServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatingController : ControllerBase
    {
        private readonly ISeatingService _seatingService;

        public SeatingController(ISeatingService seatingService)
        {
            _seatingService = seatingService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<SeatingResponse> movies = (List<SeatingResponse>)await _seatingService.GetAllSeatingsAsync();

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
    }
}
