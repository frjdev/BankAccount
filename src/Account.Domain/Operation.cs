
namespace Account.Domain;

public record Operation
{
    public Operation(int id, string? type, Account? account)
    {
        Id = id;
        Type = type;
        Account = account;
    }

    public int Id { get; set; }
    public string? Type { get; set; }
    public Account? Account { get; set; }
}
