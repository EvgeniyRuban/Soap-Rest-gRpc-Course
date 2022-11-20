using ClinicService.Domain.Constants;
using ClinicService.Domain.Entities;
using ClinicService.Domain.Models;
using ClinicService.Domain.Repos;
using ClinicService.Domain.Services;
using ClinicService.Domain.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicService.BusinessLogic.Services;

public class AuthenticationService : IAuthenticationService
{
    public const string SecretKey = "kYp3s6v9y/B?E(H+";
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ConcurrentDictionary<string, SessionContext> _sessions = new();


    public AuthenticationService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }


    public async Task<AuthenticationResponse> Login(AuthenticationRequest request, CancellationToken stoppingToken = default)
    {
        var dateTimeNow = DateTime.Now;
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();
        var accountSessionRepository = scope.ServiceProvider.GetRequiredService<IAccountSessionRepository>();

        var account =
            !string.IsNullOrWhiteSpace(request.Login) && !string.IsNullOrWhiteSpace(request.Password)
            ? await accountRepository.Get(request.Login, stoppingToken) : null;

        if (account == null ||
            !PasswordUtils.Verify(request.Password, account.PasswordHash, account.PasswordSalt))
        {
            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.Failed,
            };
        }

        var session = new AccountSession
        {
            SessionToken = CreateSessionToken(account),
            AccountId = account.Id,
            TimeCreated = dateTimeNow,
            TimeLastRequest = dateTimeNow,
            IsClosed = false,
        };

        await accountSessionRepository.Add(session, stoppingToken);

        var sessionContext = new SessionContext
        {
            SessionId = session.Id,
            SessionToken = session.SessionToken,
            Account = new AccountDto
            {
                Id = account.Id,
                Email = account.Email,
                FirstName = account.FirstName,
                Surname = account.Surname,
                Patronymic = account.Patronymic,
                Locked = account.Locked
            }
        };


        while (!_sessions.TryAdd(sessionContext.SessionToken, sessionContext)) ;

        return new AuthenticationResponse
        {
            Status = AuthenticationStatus.Success,
            SessionContext = _sessions[session.SessionToken]
        };
    }
    private string CreateSessionToken(Account account)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(SecretKey);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.Email),
                }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}