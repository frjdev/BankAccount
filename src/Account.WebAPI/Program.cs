using Account.Domain;
using Account.Infrastructure;
using Microsoft.EntityFrameworkCore;

sealed class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<IAccountRepository, AccountRepository>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var workingDirectory = Environment.CurrentDirectory;
        var DbPath = Path.Join(workingDirectory, "BankAccount.db");

        builder.Services.AddDbContext<AccountContext>(options =>
        {
            options.UseSqlite($"DataSource={DbPath}");
        });

        builder.Services.AddControllers();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.MapControllers();
        app.Run();
    }
}