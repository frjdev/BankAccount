using Account.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Account.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _accountContext;
        public AccountRepository(AccountContext accountContext) 
        { 
            _accountContext= accountContext;
        }

        public Task<ImmutableList<Operation>> GetAllTransactionsAsync()
        {
            var operationData =  _accountContext.OperationSet;
            var operationDomain = operationData.Select(x => OperationData.ToDomain(x)).ToImmutableList();

            return Task.FromResult(operationDomain);
        }

        public async Task<Domain.Account?> MakeADepositInAnAccount(int idAccount, decimal amount)
        {
            var accountData = await _accountContext.AccountSet.FirstOrDefaultAsync(x => x.Id == idAccount);

            if (accountData == null)
            {
                return null;
            }

            accountData.Balance += amount;
            await _accountContext.SaveChangesAsync();


            var operation = new OperationData() { Type = "Deposit", AccountData = accountData };

            await _accountContext.OperationSet.AddAsync(operation);
            await _accountContext.SaveChangesAsync();


            return AccountData.ToDomain(accountData);
        }

        public Task<Domain.Account?> MakeAWithdrawalInAnAccount(int idaccount, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}