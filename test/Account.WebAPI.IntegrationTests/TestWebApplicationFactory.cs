using Account.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Account.WebAPI.IntegrationTests;

public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AccountContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var workingDirectory = Environment.CurrentDirectory;
            var dataBaseDirectory = $@"{Directory.GetParent(workingDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName}\src\Temperature.WebAPI";

            var DbPath = Path.Join(dataBaseDirectory, "BankAccount.db");

            services.AddDbContext<AccountContext>(options =>
            {
                options.UseSqlite($"DataSource={DbPath}");

            });
        });

        return base.CreateHost(builder);
    }
}

