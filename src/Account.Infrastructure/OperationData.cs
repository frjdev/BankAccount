using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Account.Infrastructure
{
    public record class OperationData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Type { get; set; }
        public int AccountId { get; set; }
        public AccountData? AccountData { get; set; }

        public static Domain.Operation ToDomain(OperationData operation)
           => new Domain.Operation(operation.Id, operation.Type, AccountData.ToDomain(operation.AccountData!));

    }
}
