using Account.Domain;
using System.Collections.Immutable;

namespace Account.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository() 
        { 

        }

        public Task<ImmutableList<Operation>> GetAllTransctionsAsync()
        {
            throw new NotImplementedException();
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