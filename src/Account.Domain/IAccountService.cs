using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain
{
    public interface IAccountService
    { 
        Task<Account?> MakeADepositInAnAccount(int idAccount, decimal amount);
        Task<Account?> MakeAWithdrawalInAnAccount(int idaccount, decimal amount);
        Task<ImmutableList<Operation>> GetAllTransactionsAsync();
    }
}
