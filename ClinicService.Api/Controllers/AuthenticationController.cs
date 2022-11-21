using ClinicService.Domain.Models;
using ClinicService.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace ClinicService.Api.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;


    public AuthenticationController(IAuthenticationService authService)
    {
        ArgumentNullException.ThrowIfNull(authService, nameof(authService));
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login(
        [FromBody] AuthenticationRequest request,
        CancellationToken stoppingToken = default)
    {
        var response = await _authService.Login(request);
        if (response.Status == Domain.Constants.AuthenticationStatus.Success)
        {
            Response.Headers.Add("X-Session-Token", response.SessionContext.SessionToken);
        }
        return Ok(response);
    }

    [HttpGet("session")]
    public async Task<ActionResult<SessionContext?>> GetSession(CancellationToken stoppingToken = default)
    {
        var authorization = Request.Headers[HeaderNames.Authorization];
        SessionContext? sessionContext = null;

        if (AuthenticationHeaderValue.TryParse(authorization, out var headersValue))
        {
            var sessionToken = headersValue.Parameter;

            if (string.IsNullOrEmpty(sessionToken))
            {
                return Ok(null);
            }

            sessionContext = await _authService.GetSession(sessionToken, stoppingToken);
        }

        return Ok(sessionContext);
    }
}