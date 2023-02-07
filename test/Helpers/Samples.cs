using Account.Domain;

namespace Helpers
{
    public static class Samples
    {
        public static List<Account.Domain.Account>? accountsSamples = new()
        {
            new Account.Domain.Account(1,DateTime.Now,10,1000),
            new Account.Domain.Account(2,DateTime.Now,12,1000),
            new Account.Domain.Account(3,DateTime.Now,13,1000),
        };

    }
}