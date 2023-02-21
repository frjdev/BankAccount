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

        var accountDomain = await _AccountService.MakeADepositInAnAccountAsync(id, UpateModelAmount.Amount).ConfigureAwait(false);

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

        var resultDomain = await _AccountService.MakeAWithdrawalInAnAccountAsync(id, UpateModelAmount.Amount).ConfigureAwait(false);

        if (!resultDomain.IsSuccess || resultDomain.account == null)
        {
            return TypedResults.BadRequest(resultDomain.ErrorMessage);
        }

        var accountView = AccountView.FromDomain(resultDomain.account!);

        return TypedResults.Ok(accountView);
    }

    [HttpGet]
    public async Task<IResult> GetAllTransactionsAsync()
    {
        var transactionsDomain = await _AccountService.GetAllTransactionsAsync().ConfigureAwait(false);

        if (transactionsDomain == null)
            return TypedResults.BadRequest();

        var transactionsView = transactionsDomain.Select(x => OperationView.FromDomain(x!));

        return TypedResults.Ok(transactionsView);
    }
}
