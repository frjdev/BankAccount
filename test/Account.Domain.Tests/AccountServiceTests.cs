using Helpers;
using Moq;
using System.Collections.Immutable;
using System.Transactions;

namespace Account.Domain.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task ShouldBeAbleToMakeADepositInAnAccount()
        {
            var expected = Samples.accountsSamples!.FirstOrDefault();

            var mockBankAccountRepository = new Mock<IAccountRepository>();
            mockBankAccountRepository.Setup(x => x.MakeADepositInAnAccount(expected!.Id,10)).ReturnsAsync(expected);

            var accountService = new AccountService(mockBankAccountRepository.Object);

            var actual = await accountService.MakeADepositInAnAccount(expected!.Id,10);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldBeAbleToMakeAWithdrawalInAnAccount()
        {
            var expected = Samples.accountsSamples!.FirstOrDefault();

            var mockBankAccountRepository = new Mock<IAccountRepository>();
            mockBankAccountRepository.Setup(x => x.MakeAWithdrawalInAnAccount(expected!.Id, 10)).ReturnsAsync(expected);

            var accountService = new AccountService(mockBankAccountRepository.Object);

            var actual = await accountService.MakeAWithdrawalInAnAccount(expected!.Id, 10);

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
}