
namespace Account.Domain;

public record Account
{
    public Account(int id, DateTime date, decimal balance)
    {
        Id = id;
        Date = date;
        Balance = balance;
    }

    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Balance { get; set; }
}
