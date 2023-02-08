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

        public AccountContext(DbContextOptions<AccountContext> dbContextOptions) 
                : base(dbContextOptions)
        {

        
        }
        public DbSet<AccountData> AccountSet { get; set; } = default!;
        public DbSet<OperationData> OperationSet { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountData>().HasData(
                                             new AccountData { Id = 1, Date = DateTime.Now, Amount = 10, Balance = 1000 },
                                             new AccountData { Id = 2, Date = DateTime.Now, Amount = 10, Balance = 1000 },
                                             new AccountData { Id = 3, Date = DateTime.Now, Amount = 10, Balance = 1000 });

            modelBuilder.Entity<OperationData>().HasData(
                                            new OperationData { Id = 1, Type = "Deposit", AccountId = 1 },
                                            new OperationData { Id = 2, Type = "Deposit", AccountId = 1 },
                                            new OperationData { Id = 3, Type = "Withdrawal", AccountId = 1 },
                                            new OperationData { Id = 4, Type = "Withdrawal", AccountId = 1 },
                                            new OperationData { Id = 5, Type = "Deposit", AccountId = 1 });
        }

    }
}
