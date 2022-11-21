using ClinicService.Domain.Services;
using ClinicServiceProtos;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using static ClinicServiceProtos.AuthenticationService;

namespace ClinicService.Api.Services;

[Authorize]
public class AuthenticationServiceGrpc : AuthenticationServiceBase
{
    private readonly IAuthenticationService _authService;


    public AuthenticationServiceGrpc(IAuthenticationService authService)
    {
        _authService = authService;
    }


    [AllowAnonymous]
    public override async Task<AuthenticationResponse> Login(AuthenticationRequest request, ServerCallContext context)
    {
        var response = await _authService.Login(new Domain.Models.AuthenticationRequest
        {
            Login = request.Login,
            Password = request.Password
        }, context.CancellationToken);

        if (response is null || response.Status == Domain.Constants.AuthenticationStatus.Failed)
        {
            return new AuthenticationResponse
            {
                Status = 1,
            };
        }

        context.ResponseTrailers.Add("X-Session-Token", response.SessionContext.SessionToken);

        return new AuthenticationResponse
        {
            Status = 0,
            SessionContext = new()
            {
                SessionId = response.SessionContext.SessionId,
                SessionToken = response.SessionContext.SessionToken,
                Account = new AccountDto
                {
                    Id = response.SessionContext.Account.Id,
                    Email = response.SessionContext.Account.Email,
                    FirstName = response.SessionContext.Account.FirstName,
                    Surname = response.SessionContext.Account.Surname,
                    Patronymic = response.SessionContext.Account.Patronymic,
                    Locked = response.SessionContext.Account.Locked
                }
            }
        };
    }
    public override async Task<GetSessionResponse> GetSession(GetSessionRequest request, ServerCallContext context)
    {
        var authorizationHeaders = context.RequestHeaders.FirstOrDefault(header => header.Key == "Authorization");
        if (AuthenticationHeaderValue.TryParse(authorizationHeaders.Value, out var headerValue))
        {
            var scheme = headerValue.Scheme;
            var sessionToken = headerValue.Parameter;
            if (string.IsNullOrEmpty(sessionToken))
            {
                return new GetSessionResponse();
            }

            var sessionContext = await _authService.GetSession(sessionToken, context.CancellationToken);
            if (sessionContext is null)
            {
                return new GetSessionResponse();
            }

            return new GetSessionResponse
            {
                SessionContext = new SessionContext
                {
                    SessionId = sessionContext.SessionId,
                    SessionToken = sessionContext.SessionToken,
                    Account = new AccountDto
                    {
                        Email = sessionContext.Account.Email,
                        Id = sessionContext.Account.Id,
                        Locked = sessionContext.Account.Locked,
                        FirstName = sessionContext.Account.FirstName,
                        Surname = sessionContext.Account.Surname,
                        Patronymic = sessionContext.Account.Patronymic,
                    }
                }
            };
        }
        return new GetSessionResponse();
    }
}