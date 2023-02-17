﻿using System.Collections.Immutable;

namespace Account.Domain
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository) 
        { 
            _accountRepository= accountRepository;
        }
        public  async Task<Account?> MakeADepositInAnAccount(int idAccount,decimal amount)
        {
            return  await _accountRepository.MakeADepositInAnAccount(idAccount,amount);
        }

        public async Task<Account?> MakeAWithdrawalInAnAccount(int idAccount, decimal amount)
        {
            return await _accountRepository.MakeAWithdrawalInAnAccount(idAccount, amount);
        }

        public async Task<ImmutableList<Operation>> GetAllTransactionsAsync()
        {
            return await _accountRepository.GetAllTransactionsAsync();
        }
    }
}
