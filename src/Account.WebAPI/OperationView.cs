namespace Account.WebAPI;

public record OperationView
{
    public OperationView(
        int id,
        string? type,
        AccountView? account)
    {
        Id = id;
        Type = type;
        Account = account;
    }

    public int Id { get; }
    public string? Type { get; }
    public AccountView? Account { get; }

    public static OperationView FromDomain(Domain.Operation operationDomain)
    => new(operationDomain.Id, operationDomain.Type, AccountView.FromDomain(operationDomain.Account!));
}
