using Account.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Account.Infrastructure;

public class AccountRepository : IAccountRepository
{
    private readonly AccountContext _AccountContext;
    public AccountRepository(AccountContext accountContext)
    {
        _AccountContext = accountContext;
    }

    public Task<ImmutableList<Operation>> GetAllTransactionsAsync()
    {
        var operationData = _AccountContext.OperationSet.Include(x => x.AccountData).ToList();
        var operationDomain = operationData.Select(OperationData.ToDomain).ToImmutableList();

        return Task.FromResult(operationDomain);
    }

    public async Task<Domain.Account?> MakeADepositInAnAccountAsync(int idAccount, decimal amount)
    {
        var accountData = await _AccountContext.AccountSet.FirstOrDefaultAsync(x => x.Id == idAccount).ConfigureAwait(false);

        if (accountData == null)
        {
            return null;
        }

        accountData.Amount = amount;
        accountData.Balance += amount;
        await _AccountContext.SaveChangesAsync().ConfigureAwait(false);

        var addOperation = await AddDepositOperation(accountData).ConfigureAwait(false);

        return !addOperation ? null : AccountData.ToDomain(accountData);
    }

    public async Task<(bool IsSuccess, Domain.Account? account, string ErrorMessage)> MakeAWithdrawalInAnAccountAsync(int idAccount, decimal amount)
    {
        var accountData = await _AccountContext.AccountSet.FirstOrDefaultAsync(x => x.Id == idAccount).ConfigureAwait(false);

        if (accountData == null)
        {
            return (false, null, "account doesn't exist");
        }

        accountData.Amount = amount;

        if (accountData.Balance - amount < 0)
        {
            return (false, AccountData.ToDomain(accountData), "Insufficient funds");
        }

        accountData.Balance -= amount;
        await _AccountContext.SaveChangesAsync().ConfigureAwait(false);

        var addOperation = await AddWithdrawalOperation(accountData).ConfigureAwait(false);

        return !addOperation ? ((bool IsSuccess, Domain.Account? account, string ErrorMessage))(false, null, "Error during add Operation") : ((bool IsSuccess, Domain.Account? account, string ErrorMessage))(true, AccountData.ToDomain(accountData), string.Empty);
    }

    private async Task<bool> AddDepositOperation(AccountData accountData)
    {
        var operation = new OperationData() { Type = "Deposit", AccountData = accountData };

        return await AddOperation(operation).ConfigureAwait(false);
    }

    private async Task<bool> AddWithdrawalOperation(AccountData accountData)
    {
        var operation = new OperationData() { Type = "WithDrawal", AccountData = accountData };

        return await AddOperation(operation).ConfigureAwait(false);
    }

    private async Task<bool> AddOperation(OperationData operationData)
    {
        await _AccountContext.OperationSet.AddAsync(operationData).ConfigureAwait(false);
        var writtenState = await _AccountContext.SaveChangesAsync().ConfigureAwait(false);

        return writtenState == 1;
    }
}
