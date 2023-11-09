using Masstransit.StateMachine.Contracts.Enumns;
using MassTransit;
using Newtonsoft.Json;

namespace Masstransit.StateMachine.Sagas
{
    public class Mensagem<TEvento> : Mensagem, SagaStateMachineInstance
    {
        internal void SetEventoRecebido(TipoMensagem tipoMensagem, TEvento message)
        {
            Json = JsonConvert.SerializeObject(message);
            TipoMensagem = (int)tipoMensagem;
            DataHora = DateTime.Now;
        }
    }

    public class Mensagem
    {
        public Guid CorrelationId { get; set; }
        public DateTime DataHora { get; set; }
        public int? TipoMensagem { get; set; }
        public int StatusId { get; set; }
        public string? Json { get; set; }
    }
}
