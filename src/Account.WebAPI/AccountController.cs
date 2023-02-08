using Account.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService= accountService;
        }

        [HttpPut("Deposit/{id}")]
        public async Task<IResult> MakeADepositInAnAccount(int id, AccountUpdateModel UpateModelAmount)
        {
            if (UpateModelAmount.amount == 0)
                return TypedResults.NoContent();

           var accountDomain = await _accountService.MakeADepositInAnAccount(id, UpateModelAmount.amount);

            if (accountDomain == null)
                return TypedResults.BadRequest();

            var accountView = AccountView.FromDomain(accountDomain!);

            return TypedResults.Ok(accountView);
        }


        [HttpPut("Withdrawal/{id}")]
        public async Task<IResult> MakeAWithdrawalInAnAccount(int id, AccountUpdateModel UpateModelAmount)
        {
            if (UpateModelAmount.amount == 0)
                return TypedResults.NoContent();

            var accountDomain = await _accountService.MakeAWithdrawalInAnAccount(id, UpateModelAmount.amount);

            if (accountDomain == null)
                return TypedResults.BadRequest();

            var accountView = AccountView.FromDomain(accountDomain!);

            return TypedResults.Ok(accountView);
        }

        [HttpGet()]
        public async Task<IResult> GetAllTransactionsAsync()
        {
            var transactionsDomain = await _accountService.GetAllTransactionsAsync();

            if (transactionsDomain == null)
                return TypedResults.BadRequest();

            var transactionsView = transactionsDomain.Select(x => OperationView.FromDomain(x!));

            return TypedResults.Ok(transactionsView);
        }
    }
}
