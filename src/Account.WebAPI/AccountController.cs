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

        [HttpPost("{id}")]
        public async Task<IResult> MakeADepositInAnAccount(int idAccount, decimal amount)
        {
           var accountDomain = await _accountService.MakeADepositInAnAccount(idAccount, amount);

            if (accountDomain == null)
                return TypedResults.BadRequest();

            var accountView = AccountView.FromDomain(accountDomain!);

            return TypedResults.Ok(accountView);
        }


        [HttpPost("{id}")]
        public async Task<IResult> MakeAWithdrawalInAnAccount(int idAccount, decimal amount)
        {
            var accountDomain = await _accountService.MakeAWithdrawalInAnAccount(idAccount, amount);

            if (accountDomain == null)
                return TypedResults.BadRequest();

            var accountView = AccountView.FromDomain(accountDomain!);

            return TypedResults.Ok(accountView);
        }
    }
}
