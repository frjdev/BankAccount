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

        [HttpPost("Deposit/{id}")]
        public async Task<IResult> MakeADepositInAnAccount(int idAccount, decimal amount)
        {
           var accountDomain = await _accountService.MakeADepositInAnAccount(idAccount, amount);

            if (accountDomain == null)
                return TypedResults.BadRequest();

            var accountView = AccountView.FromDomain(accountDomain!);

            return TypedResults.Ok(accountView);
        }


        [HttpPost("Withdrawal/{id}")]
        public async Task<IResult> MakeAWithdrawalInAnAccount(int idAccount, decimal amount)
        {
            var accountDomain = await _accountService.MakeAWithdrawalInAnAccount(idAccount, amount);

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
