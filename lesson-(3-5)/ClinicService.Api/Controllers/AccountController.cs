using ClinicService.Domain.Exceptions;
using ClinicService.Domain.Models;
using ClinicService.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Api.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;


	public AccountController(IAccountService accountService)
	{
		ArgumentNullException.ThrowIfNull(accountService, nameof(accountService));
		_accountService = accountService;
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<ActionResult<CreateAccountResponse>> Create(
		[FromBody] CreateAccountRequest request,
		CancellationToken stoppingToken = default)
	{
		try
		{
			return new CreateAccountResponse
			{
				Id = await _accountService.Add(request, stoppingToken)
			};
		}
		catch (InvalidRegistrationDataException ex)
		{
			return new CreateAccountResponse
			{
				ErrorCode = ex.ErrorCode,
				ErrorMessage = ex.Message
			};
		}
		catch (Exception ex)
		{
			var exceptionTemplate = new EntityAdditionException();
			return new CreateAccountResponse
			{
				ErrorCode = exceptionTemplate.ErrorCode,
				ErrorMessage = ex.Message,
			};
		}
	}
}