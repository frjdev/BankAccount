using System.Collections.Immutable;

namespace Account.Domain
{
    public interface IAccountRepository
    {
        Task<Account?> MakeADepositInAnAccount(int idAccount, decimal amount);
        Task<Account?> MakeAWithdrawalInAnAccount(int idaccount, decimal amount);
        Task<ImmutableList<Operation>> GetAllTransactionsAsync();    
    }
}
