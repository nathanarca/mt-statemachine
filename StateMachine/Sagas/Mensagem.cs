using Masstransit.StateMachine.Contracts.Interfaces;
using MassTransit;
using Newtonsoft.Json;

namespace Masstransit.StateMachine.Sagas
{
    public class Mensagem : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public DateTime DataHora { get; set; }
        public string? Contrato { get; set; }
        public int StatusId { get; set; }
        public string? Json { get; set; }

        internal void SetEventoRecebido<TEvento>(TEvento message) where TEvento : class, IMensagem
        {
            Json = JsonConvert.SerializeObject(message);
            Contrato = typeof(TEvento).Name;
            DataHora = DateTime.Now;
        }
    }
}
