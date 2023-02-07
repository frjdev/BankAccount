using Moq;

namespace Account.Domain.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task ShouldBeAbleToMakeADepositInAnAccount()
        {
            var input = new Account(1,DateTime.Now,100,1000);
            var expected = new Account(1, DateTime.Now, 100, 1000);

            var mockBankAccountRepository = new Mock<IAccountRepository>();
            mockBankAccountRepository.Setup(x => x.MakeADepositInAnAccount(input)).ReturnsAsync(expected);

            var accountService = new AccountService(mockBankAccountRepository.Object);

            var actual = await accountService.MakeADepositInAnAccount(input);

            Assert.Equal(expected, actual);

        }
    }
}