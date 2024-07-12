using BigMoneyBanking.Repositories;
using BigMoneyBanking.Services;
using BigMoneyBanking;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string connString = builder.Configuration.GetConnectionString("BigMoneyDbConnectionString") ?? "";
        // Add services to the container.
        builder.Services.AddControllers();
        // Register the BankAccountRepository with the DI container, passing the connection string to its constructor
        // make sure add repository as a singleton so one instnce exists
        builder.Services.AddSingleton(serviceProvider =>
        {
            return new BankAccountRepository(connString);
        });
        // Register the BankAccountService with the DI container add the repository to the cnostructor
        builder.Services.AddScoped(serviceProvider =>
        {
            var repository = serviceProvider.GetRequiredService<BankAccountRepository>();
            return new BankAccountService(repository);
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}