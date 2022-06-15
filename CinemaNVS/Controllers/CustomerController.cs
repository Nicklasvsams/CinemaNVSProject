using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CustomerResponse> customers = (List<CustomerResponse>)await _customerService.GetAllCustomersAsync();

                if (customers == null)
                {
                    return StatusCode(500);
                }

                if (customers.Count == 0)
                {
                    return NoContent();
                }

                return Ok(customers);
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
                var customerResponse = await _customerService.GetCustomerByIdAsync(id);

                if (customerResponse == null)
                {
                    return NotFound();
                }

                return Ok(customerResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CustomerRequest cusReq)
        {
            try
            {
                var customerResponse = await _customerService.CreateCustomer(cusReq);

                if (customerResponse == null)
                {
                    return StatusCode(500);
                }

                return Ok(customerResponse);
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
        public async Task<IActionResult> Update([FromBody] CustomerRequest cusReq, [FromRoute] int id)
        {
            try
            {
                var customerResponse = await _customerService.UpdateCustomerByIdAsync(cusReq, id);

                if (customerResponse == null)
                {
                    return NotFound();
                }

                return Ok(customerResponse);
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
        public async Task<IActionResult> UpdateActivation([FromRoute] int id)
        {
            try
            {
                var customerResponse = await _customerService.UpdateCustomerActivationByIdAsync(id);

                if (customerResponse == null)
                {
                    return NotFound();
                }

                return Ok(customerResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
