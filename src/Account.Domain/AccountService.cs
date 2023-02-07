using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository) 
        { 
            _accountRepository= accountRepository;
        }
        public  async Task<Account> MakeADepositInAnAccount(int idAccount,decimal amount)
        {
            return  await _accountRepository.MakeADepositInAnAccount(idAccount,amount);
        }
    }
}
