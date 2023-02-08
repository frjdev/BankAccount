using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure
{
    public sealed class AccountContext: DbContext
    {
        public AccountContext() 
        {
        }

        public AccountContext(DbContextOptions<AccountContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<AccountData> AccountSet { get; set; } = default!;
        public DbSet<OperationData> OperationSet { get; set; } = default!;

    }
}
