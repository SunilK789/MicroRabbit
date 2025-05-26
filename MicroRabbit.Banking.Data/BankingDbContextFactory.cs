using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MicroRabbit.Banking.Data.Context;
using System.IO;

public class BankingDbContextFactory : IDesignTimeDbContextFactory<BankingDbContext>
{
    public BankingDbContext CreateDbContext(string[] args)
    {
        // Set up configuration to read from appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // where EF is running from
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("BankingDbConnection");

        var optionsBuilder = new DbContextOptionsBuilder<BankingDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new BankingDbContext(optionsBuilder.Options);
    }
}
