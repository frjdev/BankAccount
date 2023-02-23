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

    public async Task<bool> MakeADepositInAnAccountAsync(int idAccount, decimal amount)
    {
        var accountData = await _AccountContext.AccountSet.FirstOrDefaultAsync(x => x.Id == idAccount);

        accountData!.Amount = amount;
        accountData.Balance += amount;

        _AccountContext.AccountSet.Update(accountData);
        var writtenState = await _AccountContext.SaveChangesAsync();

        return writtenState == 1;
    }

    public async Task<Domain.Account?> GetAccountAsync(int idAccount)
    {
        var accountData = await _AccountContext.AccountSet.FirstOrDefaultAsync(x => x.Id == idAccount);

        return AccountData.ToDomain(accountData!);
    }

    public async Task<bool> MakeAWithdrawalInAnAccountAsync(int idAccount, decimal amount)
    {
        var accountData = await _AccountContext.AccountSet.FirstOrDefaultAsync(x => x.Id == idAccount);

        accountData!.Balance -= amount;

        _AccountContext.AccountSet.Update(accountData);
        var writtenState = await _AccountContext.SaveChangesAsync();

        return writtenState == 1;
    }

    public async Task<bool> AddOperationAsync(string? operation, int idAccount)
    {
        var accountData = await _AccountContext.AccountSet.FirstOrDefaultAsync(x => x.Id == idAccount);
        var operationData = new OperationData() { Type = operation, AccountData = accountData };

        await _AccountContext.OperationSet.AddAsync(operationData);
        var writtenState = await _AccountContext.SaveChangesAsync();

        return writtenState == 1;
    }
}
