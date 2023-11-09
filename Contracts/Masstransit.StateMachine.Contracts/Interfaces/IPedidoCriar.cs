namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface IPedidoCriar : IPedido
    {
    }

    public interface IPedidoCriado : IPedido
    {
       
    }

    public interface IPedidoRemovido : IPedido
    {

    }

    public interface IPedido : IMensagem
    {
        long Numero { get; set; }
        decimal Valor { get; set; }
    }
}
