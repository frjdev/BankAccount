using System.Collections.Immutable;
using System.Net;
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
}
