using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.TransactionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowingController : ControllerBase
    {
        private readonly IShowingService _showingService;

        public ShowingController(IShowingService showingService)
        {
            _showingService = showingService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<ShowingResponse> showings = (List<ShowingResponse>)await _showingService.GetAllShowingsAsync();

                if (showings == null)
                {
                    return StatusCode(500);
                }

                if (showings.Count == 0)
                {
                    return NoContent();
                }

                return Ok(showings);
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
                var showingResponse = await _showingService.GetShowingByIdAsync(id);

                if (showingResponse == null)
                {
                    return NotFound();
                }

                return Ok(showingResponse);
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
        public async Task<IActionResult> Create([FromBody] ShowingRequest shoReq)
        {
            try
            {
                var showingResponse = await _showingService.CreateShowingAsync(shoReq);

                if (showingResponse == null)
                {
                    return StatusCode(500);
                }

                return Ok(showingResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] ShowingRequest shoReq, [FromRoute] int id)
        {
            try
            {
                var showingResponse = await _showingService.UpdateShowingByIdAsync(shoReq, id);

                if (showingResponse == null)
                {
                    return NotFound();
                }

                return Ok(showingResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var showingResponse = await _showingService.DeleteShowingByIdAsync(id);

                if (showingResponse == null)
                {
                    return NotFound();
                }

                return Ok(showingResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
