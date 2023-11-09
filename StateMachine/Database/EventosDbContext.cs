using Masstransit.StateMachine.Contracts.Enumns;
using Masstransit.StateMachine.Contracts.Interfaces;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mensagem>(entity =>
            {
                entity.HasDiscriminator().HasValue(0);
                entity.HasKey(e => e.CorrelationId);
                entity.ToTable("Mensagens");
            });

            modelBuilder.ConfigureMensagemType<IPedidoCriado>(TipoMensagem.IPedidoCriado);
            modelBuilder.ConfigureMensagemType<IPedidoCriar>(TipoMensagem.IPedidoCriar);
            modelBuilder.ConfigureMensagemType<IPedidoRemovido>(TipoMensagem.IPedidoRemovido);
        }

        protected override IEnumerable<ISagaClassMap> Configurations => new List<ISagaClassMap>();
    }

    public static class SagaDbContextExtension
    {
        public static void ConfigureMensagemType<TEvento>(this ModelBuilder modelBuilder, TipoMensagem tipoEvento)
        {
            modelBuilder.Entity<Mensagem<TEvento>>().HasBaseType<Mensagem>()
                .HasDiscriminator(n => n.TipoMensagem).HasValue((int)tipoEvento);
        }
    }
}