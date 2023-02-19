namespace Account.WebAPI;

public record AccountView
{
    public int Id { get; }
    public DateTime Date { get; }
    public decimal Amount { get; }
    public decimal Balance { get; }
    public AccountView(int id, DateTime date, decimal amount, decimal balance)
    {
        Id = id;
        Date = date;
        Amount = amount;
        Balance = balance;
    }
    public static AccountView FromDomain(Domain.Account accountDomain)
      => new(accountDomain.Id, accountDomain.Date, accountDomain.Amount, accountDomain.Balance);
}
