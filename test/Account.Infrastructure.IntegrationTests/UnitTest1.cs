using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.IntegrationTests;

public class AccountRepositoryTest
{
    private static DbContextOptions<AccountContext> ConnectToSqLiteDatabaseProduction()
    {
        var workingDirectory = Environment.CurrentDirectory;
        var dataBaseDirectory = $@"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName}\src\Account.WebAPI";

        var DbPath = Path.Join(dataBaseDirectory, "BankAccount.db");

        var _contextOptions = new DbContextOptionsBuilder<AccountContext>()
                        .UseSqlite($"DataSource={DbPath}")
                        .Options;

        return _contextOptions;
    }
}
