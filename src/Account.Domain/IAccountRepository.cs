using System.Collections.Immutable;

namespace Account.Domain
{
    public interface IAccountRepository
    {
        Task<Account?> MakeADepositInAnAccountAsync(int idAccount, decimal amount);
        Task<(bool IsSuccess, Account? account, string ErrorMessage)> MakeAWithdrawalInAnAccountAsync(int idAccount, decimal amount);
        Task<ImmutableList<Operation>> GetAllTransactionsAsync();    
    }
}
