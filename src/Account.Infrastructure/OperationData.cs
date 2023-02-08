using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure
{
    public record class OperationData
    {
        public OperationData(int id, string? type, AccountData? accountData)
        {
            Id = id;
            Type = type;
            AccountData = accountData;
        }

        public int Id { get; set; }
        public string? Type { get; set; }
        public AccountData? AccountData { get; set; }

        public static Domain.Operation ToDomain(OperationData operation)
           => new Domain.Operation(operation.Id, operation.Type, AccountData.ToDomain(operation.AccountData!));

    }
}
