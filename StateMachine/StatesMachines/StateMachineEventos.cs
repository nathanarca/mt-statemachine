using Masstransit.StateMachine.Contracts.Enumns;
using Masstransit.StateMachine.Contracts.Interfaces;

namespace Masstransit.StateMachine.States
{
    public class StateCriarPedido : StateMachineBase<IPedidoCriar>
    {
        public StateCriarPedido() : base("pedido_criar")
        {
        }

        public override TipoMensagem TipoMensagem => TipoMensagem.IPedidoCriar;
    }

    public class StatePedidoCriado : StateMachineBase<IPedidoCriado>
    {
        public StatePedidoCriado() : base("pedido_criado")
        {
        }

        public override TipoMensagem TipoMensagem => TipoMensagem.IPedidoCriado;
    }

    public class StatePedidoRemovido : StateMachineBase<IPedidoRemovido>
    {
        public StatePedidoRemovido() : base("pedido_removido")
        {
        }

        public override TipoMensagem TipoMensagem => TipoMensagem.IPedidoRemovido;
    }
}
