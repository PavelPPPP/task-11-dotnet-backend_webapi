using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ModelApi.Services.DataSource
{
    public class SelfFinanceDbContextFactory : IDesignTimeDbContextFactory<SelfFinanceDbContext>
    {
        public SelfFinanceDbContext CreateDbContext(string[] args)
        {
            //ConfigurationBuilder builder = new ConfigurationBuilder();
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            //builder.AddJsonFile("appsettings.json");
            //IConfigurationRoot config = builder.Build();


            var optionsBuilder = new DbContextOptionsBuilder<SelfFinanceDbContext>();
            //string connectionString = "Server=(localdb)\\mssqllocaldb;Database=SelfFinance;Trusted_Connection=True;";
            string connectionString = "Server=tcp:pavlopi.database.windows.net,1433;Initial Catalog=SelfFinance;Persist Security Info=False;User ID=pavlopi;Password=devarT001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString);
            return new SelfFinanceDbContext(optionsBuilder.Options);
        }
    }
}
