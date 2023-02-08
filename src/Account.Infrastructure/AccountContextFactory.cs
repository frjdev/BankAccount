using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure
{
    public class AccountContextFactory : IDesignTimeDbContextFactory<AccountContext>
    {
        public AccountContext CreateDbContext(string[] args)
        {

            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var DbPath = Path.Join(path, "BankAccount.db");

            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>();
            optionsBuilder.UseSqlite($"DataSource={DbPath}");

            return new AccountContext(optionsBuilder.Options);
        }
    }
}
