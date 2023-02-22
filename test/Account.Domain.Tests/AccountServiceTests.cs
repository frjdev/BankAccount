using Helpers;
using Moq;
using System.Collections.Immutable;

namespace Account.Domain.Tests;

public class AccountServiceTests
{
    [Fact]
    public async Task ShouldBeAbleToMakeADepositInAnAccount()
    {
        var expected = Samples.accountsSamples!.FirstOrDefault();

        var mockBankAccountRepository = new Mock<IAccountRepository>();
        mockBankAccountRepository.Setup(x => x.MakeADepositInAnAccountAsync(expected!.Id, 10)).ReturnsAsync(expected);

        var accountService = new AccountService(mockBankAccountRepository.Object);

        var actual = await accountService.MakeADepositInAnAccountAsync(expected!.Id, 10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ShouldBeAbleToMakeAWithdrawalInAnAccount()
    {
        var account = Samples.accountsSamples!.FirstOrDefault();
        var expected = (true, account, string.Empty);

        var mockBankAccountRepository = new Mock<IAccountRepository>();
        mockBankAccountRepository.Setup(x => x.MakeAWithdrawalInAnAccountAsync(expected!.account!.Id, 10)).ReturnsAsync(expected);

        var accountService = new AccountService(mockBankAccountRepository.Object);

        var actual = await accountService.MakeAWithdrawalInAnAccountAsync(expected!.account!.Id, 10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllTransactions()
    {
        var expected = Samples.transctionSamples!.ToImmutableList();

        var mockBankAccountRepository = new Mock<IAccountRepository>();
        mockBankAccountRepository.Setup(x => x.GetAllTransactionsAsync()).Returns(Task.FromResult(expected)!);

        var account = new AccountService(mockBankAccountRepository.Object);
        var actual = await account.GetAllTransactionsAsync();

        Assert.Equal(actual, expected);
    }
}
