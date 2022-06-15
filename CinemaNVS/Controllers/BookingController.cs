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
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
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
                List<BookingResponse> bookings = (List<BookingResponse>)await _bookingService.GetAllBookingsAsync();

                if (bookings == null)
                {
                    return StatusCode(500);
                }

                if (bookings.Count == 0)
                {
                    return NoContent();
                }

                return Ok(bookings);
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
                var bookingResponse = await _bookingService.GetBookingByIdAsync(id);

                if (bookingResponse == null)
                {
                    return NotFound();
                }

                return Ok(bookingResponse);
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
        public async Task<IActionResult> Create([FromBody] BookingRequest booReq)
        {
            try
            {
                var bookingResponse = await _bookingService.CreateBookingAsync(booReq);

                if (bookingResponse == null)
                {
                    return StatusCode(500);
                }

                return Ok(bookingResponse);
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
        public async Task<IActionResult> Update([FromBody] BookingRequest booReq, [FromRoute] int id)
        {
            try
            {
                var bookingResponse = await _bookingService.UpdateBookingByIdAsync(booReq ,id);

                if (bookingResponse == null)
                {
                    return NotFound();
                }

                return Ok(bookingResponse);
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
                var bookingResponse = await _bookingService.DeleteBookingByIdAsync(id);

                if (bookingResponse == null)
                {
                    return NotFound();
                }

                return Ok(bookingResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
