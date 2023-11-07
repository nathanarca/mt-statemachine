namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface ICriarPedido : IPedido
    {
    }

    public interface IPedidoCriado : IPedido
    {
       
    }

    public interface IPedido : IMensagem
    {
        long Numero { get; set; }
        decimal Valor { get; set; }
    }
}
