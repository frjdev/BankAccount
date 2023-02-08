using Account.Domain;
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

        public Task<Domain.Account?> MakeADepositInAnAccount(int idAccount, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Account?> MakeAWithdrawalInAnAccount(int idaccount, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}