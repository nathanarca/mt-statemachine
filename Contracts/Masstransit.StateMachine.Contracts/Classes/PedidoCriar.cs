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


    public class PedidoCriar : Pedido, IPedidoCriar
    {
    }

    public class PedidoCriado : Pedido, IPedidoCriado
    {
    }

    public class PedidoRemovido : Pedido, IPedidoRemovido
    {
    }

    public class Sucesso<TEvento> : ISucesso<TEvento> where TEvento : IMensagem
    {
        public Sucesso(TEvento evento)
        {
            Evento = evento;
        }
        public TEvento Evento { get; set; }
    }
}
