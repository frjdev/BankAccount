using Account.Domain;
using Account.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddDbContext<AccountContext>(options =>
{
    options.UseSqlite($"DataSource=BankAccount.db;");
});

builder.Services.AddControllers();


app.MapControllers();
app.Run();
