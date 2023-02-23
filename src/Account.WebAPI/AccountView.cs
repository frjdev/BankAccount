namespace Account.WebAPI;

public record AccountView
{
    public int Id { get; }
    public DateTime Date { get; }
    public decimal Balance { get; }
    public AccountView(int id, DateTime date, decimal balance)
    {
        Id = id;
        Date = date;
        Balance = balance;
    }
    public static AccountView FromDomain(Domain.Account accountDomain)
      => new(accountDomain.Id, accountDomain.Date, accountDomain.Balance);
}
