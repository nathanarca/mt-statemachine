using Masstransit.StateMachine.Sagas;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masstransit.StateMachine.Database.Maps
{
    public class SagaMap : SagaClassMap<Mensagem>
    {
        protected override void Configure(EntityTypeBuilder<Mensagem> entity, ModelBuilder model)
        {
            entity.ToTable("Mensagens");
        }
    }
}