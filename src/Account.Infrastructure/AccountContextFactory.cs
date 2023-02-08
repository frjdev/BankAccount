using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure
{
    public class AccountContextFactory : IDesignTimeDbContextFactory<AccountContext>
    {
        public AccountContext CreateDbContext(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string dataBaseDirectory = $@"{Directory.GetParent(workingDirectory)!.FullName}\Account.WebAPI";

            var DbPath = Path.Join(dataBaseDirectory, "BankAccount.db");

            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>();
            optionsBuilder.UseSqlite($"DataSource={DbPath}");

            return new AccountContext(optionsBuilder.Options);
        }
    }
}
