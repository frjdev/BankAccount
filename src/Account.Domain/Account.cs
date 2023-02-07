using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain
{
    public record Account
    {
        public Account(int id, DateTime date, decimal amount, decimal balance)
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
    }
}
