namespace Account.Domain.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public void ShouldBeAbleToMakeADepositInAnAccount()
        {
            var input = new Account(1,DateTime.Now,100,1000);
            //var expected = new Account(1, DateTime.Now, 100, 1000);

            var accountService = new AccountService();
            var actual = accountService.MakeADepositInAnAccount(input);

            Assert.Equal(input, actual);

        }
    }
}