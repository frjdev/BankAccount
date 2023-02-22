using Account.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebAPI;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _AccountService;
    public AccountController(IAccountService accountService)
    {
        _AccountService = accountService;
    }

    [HttpPut("Deposit/{id}")]
    public async Task<IResult> MakeADepositInAnAccount(int id, AccountUpdateModel UpateModelAmount)
    {
        if (UpateModelAmount.Amount == 0)
        {
            return TypedResults.NoContent();
        }

        var accountDomain = await _AccountService.MakeADepositInAnAccountAsync(id, UpateModelAmount.Amount);

        if (accountDomain == null)
        {
            return TypedResults.BadRequest();
        }

        var accountView = AccountView.FromDomain(accountDomain!);

        return TypedResults.Ok(accountView);
    }

    [HttpPut("Withdrawal/{id}")]
    public async Task<IResult> MakeAWithdrawalInAnAccount(int id, AccountUpdateModel UpateModelAmount)
    {
        if (UpateModelAmount.Amount == 0)
        {
            return TypedResults.NoContent();
        }

        var (IsSuccess, account, ErrorMessage) = await _AccountService.MakeAWithdrawalInAnAccountAsync(id, UpateModelAmount.Amount);

        if (!IsSuccess || account == null)
        {
            return TypedResults.BadRequest(ErrorMessage);
        }

        var accountView = AccountView.FromDomain(account!);

        return TypedResults.Ok(accountView);
    }

    [HttpGet]
    public async Task<IResult> GetAllTransactionsAsync()
    {
        var transactionsDomain = await _AccountService.GetAllTransactionsAsync();

        if (transactionsDomain == null)
        {
            return TypedResults.BadRequest();
        }

        var transactionsView = transactionsDomain.Select(x => OperationView.FromDomain(x!));

        return TypedResults.Ok(transactionsView);
    }
}
