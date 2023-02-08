using Account.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebAPI
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService= accountService;
        }

    }
}
