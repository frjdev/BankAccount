using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure
{
    public class AccountContextFactory : IDesignTimeDbContextFactory<AccountContext>
    {
        public AccountContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>();
            optionsBuilder.UseSqlite($"DataSource=BankAccount.db;");

            return new AccountContext(optionsBuilder.Options);
        }
    }
}
