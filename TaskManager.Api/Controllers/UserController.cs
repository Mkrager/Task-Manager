using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Features.Account.Commads.Registration;
using TaskManager.Application.Features.Account.Queries.Authentication;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(IMediator mediator) : Controller
    {
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            var dtos = await mediator.Send(new AuthenticationQuery()
            {
                Email = request.Email,
                Password = request.Password
            });

            return Ok(dtos);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterAsync(RegistrationRequest request)
        {
            var dtos = await mediator.Send(new RegistrationCommand()
            {
                UserName = request.Username,
                Password = request.Password,
                Email = request.Email,
            });

            return Ok(dtos);
        }
    }
}