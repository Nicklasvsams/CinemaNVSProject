using CinemaNVS.Models;
using CinemasNVS.BLL.DTOs;
using CinemasNVS.BLL.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CinemaNVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly JWTSetting _jWTSetting;

        public LoginController(ILoginService loginService, IOptions<JWTSetting> jWTSetting)
        {
            _loginService = loginService;
            _jWTSetting = jWTSetting.Value;
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

        [HttpGet("{username}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUsername([FromRoute] string username)
        {
            try
            {
                var loginResponse = await _loginService.GetLoginByUsernameAsync(username);

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

        [HttpGet("{username}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthorizeLogin([FromRoute] string username, [FromRoute] string password)
        {
            try
            {
                var loginResponse = await _loginService.AuthorizeLoginAsync(username, password);

                if (loginResponse == null)
                {
                    return Unauthorized();
                };

                var tokenhandler = new JwtSecurityTokenHandler();//Reads and validates a 'JSON Web Token' (JWT) 
                var tokenkey = System.Text.Encoding.UTF8.GetBytes(_jWTSetting.SecretKey); // unicode characters => sequence of bytes, 16 bit til 8 bit system
                var tokenDescriptor = new SecurityTokenDescriptor // Represents the cryptographic key and security algorithms that are used to generate a digital signature.
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                                new Claim(ClaimTypes.Name, loginResponse.Username),
                                new Claim(ClaimTypes.Role, loginResponse.IsAdmin ? "Admin" : "User")
                        }
                    ),
                    Expires = DateTime.Now.AddHours(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenhandler.CreateToken(tokenDescriptor);
                var finalToken = tokenhandler.WriteToken(token);
                
                return Ok(new AuthorizedLogin(loginResponse, finalToken));
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
        [Authorize]
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
        [Authorize(Roles = "Admin")]
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
