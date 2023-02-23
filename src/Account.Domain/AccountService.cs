using System.Collections.Immutable;

namespace Account.Domain;

public class AccountService : IAccountService
{
    public readonly IAccountRepository _accountRepository;
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task<(bool IsSuccess, Account? account, string ErrorMessage)> MakeADepositInAnAccountAsync(int idAccount, decimal amount)
    {
        var account = await _accountRepository.GetAccountAsync(idAccount);

        if (account == null)
        {
            return (false, null, "account doesn't exist");
        }

        var IsSuccss = await _accountRepository.MakeADepositInAnAccountAsync(idAccount, amount);

        if (!IsSuccss)
        {
            return (false, account, "Error during the deposit");
        }

        var addOperation = await AddDepositOperation(account.Id);

        return !addOperation ?
                    (false, null, "Error during add Operation")
                    :
                    (true, account, string.Empty);

    }
    public async Task<(bool IsSuccess, Account? account, string ErrorMessage)> MakeAWithdrawalInAnAccountAsync(int idAccount, decimal amount)
    {
        var account = await _accountRepository.GetAccountAsync(idAccount);

        if (account == null)
        {
            return (false, null, "account doesn't exist");
        }

        if (account.Balance - amount < 0)
        {
            return (false, account, "Insufficient funds");
        }

        var IsSuccss = await _accountRepository.MakeAWithdrawalInAnAccountAsync(idAccount, amount);

        if (!IsSuccss)
        {
            return (false, account, "Error during the withdrawal");
        }

        var addOperation = await AddWithdrawalOperation(account.Id);

        return !addOperation ?
                    (false, null, "Error during add Operation")
                    :
                    (true, account, string.Empty);
    }

    public async Task<ImmutableList<Operation>> GetAllTransactionsAsync()
    {
        return await _accountRepository.GetAllTransactionsAsync();
    }

    private async Task<bool> AddWithdrawalOperation(int idAccount)
    {
        return await _accountRepository.AddOperationAsync("WithDrawal", idAccount);
    }

    private async Task<bool> AddDepositOperation(int idAccount)
    {
        return await _accountRepository.AddOperationAsync("Deposit", idAccount);
    }
}
