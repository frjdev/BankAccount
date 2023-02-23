using System.Collections.Immutable;

namespace Account.Domain;

public interface IAccountService
{
    Task<(bool IsSuccess, Account? account, string ErrorMessage)> MakeADepositInAnAccountAsync(int idAccount, decimal amount);
    Task<(bool IsSuccess, Account? account, string ErrorMessage)> MakeAWithdrawalInAnAccountAsync(int idAccount, decimal amount);
    Task<ImmutableList<Operation>> GetAllTransactionsAsync();
}
