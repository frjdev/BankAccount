using System.Collections.Immutable;
using System.Net;
using System.Text;
using Account.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Account.WebAPI.IntegrationTests;
public class AccountControllerTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _HttpClient;
    private const string RequestBaseUri = "http://localhost/Account";

    public AccountControllerTests(TestWebApplicationFactory<Program> factory)
    {
        _HttpClient = factory.CreateClient();
    }

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

    [Fact]
    public async Task ShouldBeAbleToReturnAllTransactions()
    {
        var httpResponse = await _HttpClient.GetAsync(RequestBaseUri);

        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        var responseAsString = await httpResponse.Content.ReadAsStringAsync();
        Assert.NotNull(responseAsString);

        var actualValue = JsonConvert.DeserializeObject<ImmutableList<OperationView>>(responseAsString);

        Assert.NotNull(actualValue);
    }

    [Fact]
    public async Task ShouldBeAbleToMakeADeposit()
    {
        var options = ConnectToSqLiteDatabaseProduction();

        await using var accountContext = new AccountContext(options);

        var account = new AccountData { Date = DateTime.Now, Amount = 10, Balance = 30 };
        await accountContext.AddRangeAsync(account);
        await accountContext.SaveChangesAsync();


        var updateRequestModel = new AccountUpdateModel
        {
            Amount = 10
        };
        var content = JsonConvert.SerializeObject(updateRequestModel);

        var requestBody = new StringContent(content, Encoding.UTF8, "application/json");
        var httpResponse = await _HttpClient.PutAsync($@"{RequestBaseUri}\Deposit\{account.Id}", requestBody);

        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        var responseAsString = await httpResponse.Content.ReadAsStringAsync();
        Assert.NotNull(responseAsString);

        var actualValue = JsonConvert.DeserializeObject<AccountView>(responseAsString);

        Assert.NotNull(actualValue);
    }

    [Fact]
    public async Task ShouldBeAbleToMakeAWithDrawal()
    {
        var options = ConnectToSqLiteDatabaseProduction();

        await using var accountContext = new AccountContext(options);

        var account = new AccountData { Date = DateTime.Now, Amount = 10, Balance = 30 };
        await accountContext.AddRangeAsync(account);
        await accountContext.SaveChangesAsync();


        var updateRequestModel = new AccountUpdateModel
        {
            Amount = 10
        };
        var content = JsonConvert.SerializeObject(updateRequestModel);

        var requestBody = new StringContent(content, Encoding.UTF8, "application/json");
        var httpResponse = await _HttpClient.PutAsync($@"{RequestBaseUri}\Withdrawal\{account.Id}", requestBody);

        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        var responseAsString = await httpResponse.Content.ReadAsStringAsync();
        Assert.NotNull(responseAsString);

        var actualValue = JsonConvert.DeserializeObject<AccountView>(responseAsString);

        Assert.NotNull(actualValue);
    }

    [Fact]
    public async Task ShouldBeNotAbleToMakeAWithDrawal()
    {
        var options = ConnectToSqLiteDatabaseProduction();

        await using var accountContext = new AccountContext(options);

        var account = new AccountData { Date = DateTime.Now, Amount = 10, Balance = 0 };
        await accountContext.AddRangeAsync(account);
        await accountContext.SaveChangesAsync();


        var updateRequestModel = new AccountUpdateModel
        {
            Amount = 10
        };
        var content = JsonConvert.SerializeObject(updateRequestModel);

        var requestBody = new StringContent(content, Encoding.UTF8, "application/json");
        var httpResponse = await _HttpClient.PutAsync($@"{RequestBaseUri}\Withdrawal\{account.Id}", requestBody);

        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);

        var responseAsString = await httpResponse.Content.ReadAsStringAsync();
        Assert.NotNull(responseAsString);
        var actualValue = JsonConvert.DeserializeObject<string>(responseAsString);

        Assert.Equal("Insufficient funds", actualValue);
    }
}
