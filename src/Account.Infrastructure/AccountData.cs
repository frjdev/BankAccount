using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure
{
    public record AccountData
    {
        public AccountData(int id, DateTime date, decimal amount, decimal balance)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Balance = balance;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

        public static Domain.Account ToDomain(AccountData account)
            => new Domain.Account(account.Id, account.Date, account.Amount, account.Balance);
    }
}
