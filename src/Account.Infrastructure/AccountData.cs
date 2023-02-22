using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Account.Infrastructure;

public record AccountData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public List<OperationData>? Operations { get; set; }

    public static Domain.Account ToDomain(AccountData account)
        => new Domain.Account(account.Id, account.Date, account.Balance);
}
