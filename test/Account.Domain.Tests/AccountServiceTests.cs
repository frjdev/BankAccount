using Helpers;
using Moq;
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
        public void ShouldBeAbleToGetAllTransactions()
        {
            var expected = new List<Operation>();


            var mockBankAccountRepository = new Mock<IAccountRepository>();
            mockBankAccountRepository.Setup(x => x.GetAllTransctionsAsync()).ReturnsAsync(expected);

            var account = new AccountService(mockBankAccountRepository.Object);
            var actual = account.GetAllTransctionsAsync();

            Assert.Equal(actual, expected);
        }
    }
}