using Masstransit.StateMachine.Contracts.Interfaces;

namespace Masstransit.StateMachine.Contratos.Classes
{

    public class Pedido : IPedido
    {
        public Guid Identificador { get; set; }
        public DateTime TimeStamp { get; set; }
        public long Numero { get; set; }
        public decimal Valor { get; set; }
    }


    public class CriarPedido : Pedido, ICriarPedido
    {
    }

    public class PedidoCriado: Pedido, IPedidoCriado
    {
    }

    public class Sucesso<TEvento> : ISucesso<TEvento> where TEvento : class, IMensagem
    {
        public Sucesso(TEvento evento)
        {
            Evento = evento;
            Identificador = evento.Identificador;
            TimeStamp = evento.TimeStamp;
        }

        public Guid Identificador { get; set; }
        public DateTime TimeStamp { get; set; }
        public TEvento Evento { get; set; }
    }
}
