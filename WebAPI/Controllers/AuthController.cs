using Business.Features.Auth.Commands.Login;
using Business.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _meditor;

        public AuthController(IMediator meditor)
        {
            _meditor = meditor;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            await _meditor.Send(registerCommand);
            return Created();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            await _meditor.Send(loginCommand);
            return Ok();
        }
    }
}
