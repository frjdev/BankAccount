using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain
{
    public interface IAccountRepository
    {
        Task<Account> MakeADepositInAnAccount(Account account);
            
    }
}
