using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;
using ClinicService.Domain.Models;
using ClinicService.Domain.Repos;
using ClinicService.Domain.Services;
using ClinicService.Domain.Utils;

namespace ClinicService.BusinessLogic.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;


    public AccountService(IAccountRepository accountRepository)
    {
        ArgumentNullException.ThrowIfNull(accountRepository, nameof(accountRepository));
        _accountRepository = accountRepository;
    }


    public async Task<int> Add(CreateAccountRequest request, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        if (string.IsNullOrWhiteSpace(request.Email)
            || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new InvalidRegistrationDataException();
        }

        var tuglePassword = PasswordUtils.CreatePasswordHash(request.Password);
        return await _accountRepository.Add(new Account
        {
            Email = request.Email,
            PasswordHash = tuglePassword.passwordHash,
            PasswordSalt = tuglePassword.passwordSalt,
            FirstName = request.FirstName,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
        }, stoppingToken);
    }
}