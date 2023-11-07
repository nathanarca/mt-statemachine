using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Masstransit.StateMachine.Database
{
    public class EventosDbContextFactory : IDesignTimeDbContextFactory<EventosDbContext>
    {
        public EventosDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EventosDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));

            return new EventosDbContext(optionsBuilder.Options);
        }
    }
}