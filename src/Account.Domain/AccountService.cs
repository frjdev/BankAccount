using System.Collections.Immutable;

namespace Account.Domain;

public class AccountService : IAccountService
{
    public readonly IAccountRepository _accountRepository;
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task<Account?> MakeADepositInAnAccountAsync(int idAccount, decimal amount)
    {
        return await _accountRepository.MakeADepositInAnAccountAsync(idAccount, amount).ConfigureAwait(true);
    }
    public async Task<(bool IsSuccess, Account? account, string ErrorMessage)> MakeAWithdrawalInAnAccountAsync(int idAccount, decimal amount)
    {
        return await _accountRepository.MakeAWithdrawalInAnAccountAsync(idAccount, amount).ConfigureAwait(true);
    }

    public async Task<ImmutableList<Operation>> GetAllTransactionsAsync()
    {
        return await _accountRepository.GetAllTransactionsAsync().ConfigureAwait(true);
    }
}
