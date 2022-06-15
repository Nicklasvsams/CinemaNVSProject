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
    public class BookingSeatingController : ControllerBase
    {
        private readonly IBookingSeatingService _bookingSeatingService;

        public BookingSeatingController(IBookingSeatingService bookingSeatingService)
        {
            _bookingSeatingService = bookingSeatingService;
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
                List<BookingSeatingResponse> actors = (List<BookingSeatingResponse>)await _bookingSeatingService.GetAllBookingSeatingsAsync();

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
        public async Task<IActionResult> Create([FromBody] BookingSeatingRequest booSeaReq)
        {
            try
            {
                var bookingSeatingResponse = await _bookingSeatingService.CreateBookingSeatingAsync(booSeaReq);

                if (bookingSeatingResponse == null)
                {
                    return StatusCode(500);
                }

                return Ok(bookingSeatingResponse);
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
                var bookingSeatingResponse = await _bookingSeatingService.DeleteBookingSeatingAsync(id);

                if (bookingSeatingResponse == null)
                {
                    return NotFound();
                }

                return Ok(bookingSeatingResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
