using Account.Domain;
using Account.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();



string workingDirectory = Environment.CurrentDirectory;

var DbPath = Path.Join(workingDirectory, "BankAccount.db");

builder.Services.AddDbContext<AccountContext>(options =>
{
    options.UseSqlite($"DataSource={DbPath}");
});

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();
