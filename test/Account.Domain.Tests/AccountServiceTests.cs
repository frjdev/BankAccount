using Helpers;
using Moq;

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
    }
}