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
}
