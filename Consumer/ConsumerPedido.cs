using Masstransit.StateMachine.Contracts.Interfaces;
using Masstransit.StateMachine.Contratos.Classes;
using MassTransit;

namespace Masstransit.StateMachine
{
    public class ConsumerPedido<TEvento> : IConsumer<TEvento> where TEvento : class, IMensagem
    {
        public async Task Consume(ConsumeContext<TEvento> context)
        {
            await context.Send<ISucesso<TEvento>>(Configuracao.UriSaga, new { Evento = context.Message });

            await context.ConsumeCompleted;
        }
    }
}