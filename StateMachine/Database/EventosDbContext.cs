using Masstransit.StateMachine.Database.Maps;
using Masstransit.StateMachine.Sagas;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Masstransit.StateMachine.Database
{
    public class EventosDbContext : SagaDbContext
    {
        public EventosDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new SagaMap(); }
        }

        DbSet<Mensagem> Mensagens { get; set; }
    }
}