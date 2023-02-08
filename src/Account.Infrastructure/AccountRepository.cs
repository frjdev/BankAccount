using Account.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Security.Principal;

namespace Account.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        public readonly AccountContext _accountContext;
        public AccountRepository(AccountContext accountContext) 
        { 
            _accountContext= accountContext;
        }

        public Task<ImmutableList<Operation>> GetAllTransactionsAsync()
        {
            var operationData =  _accountContext.OperationSet.Include(x => x.AccountData).ToList();
            var operationDomain = operationData.Select(x => OperationData.ToDomain(x)).ToImmutableList();

            return Task.FromResult(operationDomain);
        }

        public async Task<Domain.Account?> MakeADepositInAnAccount(int idAccount, decimal amount)
        {
            var accountData = await _accountContext.AccountSet.FirstOrDefaultAsync(x => x.Id == idAccount);

            if (accountData == null)
                return null;

            accountData.Balance += amount;
            await _accountContext.SaveChangesAsync();


            var addOperation = await AddDepositOperation(accountData);

            if (!addOperation)
                return null;

            return AccountData.ToDomain(accountData);
        }

        public async Task<Domain.Account?> MakeAWithdrawalInAnAccount(int idAccount, decimal amount)
        {
            var accountData = await _accountContext.AccountSet.FirstOrDefaultAsync(x => x.Id == idAccount);

            if (accountData == null)
                return null;

            accountData.Balance -= amount;
            await _accountContext.SaveChangesAsync();


            var addOperation = await AddWithdrawalOperation(accountData);

            if (!addOperation)
                return null;

            return AccountData.ToDomain(accountData);
        }

        private async Task<bool> AddDepositOperation(AccountData accountData)
        {
            var operation = new OperationData() { Type = "Deposit", AccountData = accountData };

            return await AddOperation(operation);
        }

        private async Task<bool> AddWithdrawalOperation(AccountData accountData)
        {
            var operation = new OperationData() { Type = "WithDrawal", AccountData = accountData };

            return  await AddOperation(operation);
        }

        private async Task<bool> AddOperation(OperationData operationData)
        {
            await _accountContext.OperationSet.AddAsync(operationData);
            var writtenState = await _accountContext.SaveChangesAsync();

            return writtenState == 1;
        }
    }
}