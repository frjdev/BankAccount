using System.Collections.Immutable;

namespace Account.Domain;

public interface IAccountRepository
{
    Task<Account?> GetAccountAsync(int idAccount);
    Task<bool> MakeADepositInAnAccountAsync(int idAccount, decimal amount);
    Task<bool> MakeAWithdrawalInAnAccountAsync(int idAccount, decimal amount);
    Task<ImmutableList<Operation>> GetAllTransactionsAsync();
    Task<bool> AddOperationAsync(string operation, int idAccount);
}
