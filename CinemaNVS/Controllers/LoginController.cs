using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<LoginResponse> logins = (List<LoginResponse>)await _loginService.GetAllLoginsAsync();

                if (logins == null)
                {
                    return StatusCode(500);
                }

                if (logins.Count == 0)
                {
                    return NoContent();
                }

                return Ok(logins);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{username}, {password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthorizeByName([FromRoute] string username, [FromRoute] string password)
        {
            try
            {
                var loginResponse = await _loginService.AuthorizeLoginByUsernameAsync(username, password);

                if (loginResponse == null)
                {
                    return NotFound();
                }

                return Ok(loginResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] LoginRequest logReq)
        {
            try
            {
                var loginResponse = await _loginService.CreateLoginAsync(logReq);

                if (loginResponse == null)
                {
                    return StatusCode(500);
                }

                return Ok(loginResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] LoginRequest actReq, [FromRoute] string username)
        {
            try
            {
                var loginResponse = await _loginService.UpdateLoginByUsernameAsync(actReq, username);

                if (loginResponse == null)
                {
                    return NotFound();
                }

                return Ok(loginResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] string username)
        {
            try
            {
                var loginResponse = await _loginService.DeleteLoginByUsernameAsync(username);

                if (loginResponse == null)
                {
                    return NotFound();
                }

                return Ok(loginResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
