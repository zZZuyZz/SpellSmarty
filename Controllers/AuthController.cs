﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpellSmarty.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesDefaultResponseType(typeof(AuthResponseDto))]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignUpCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
