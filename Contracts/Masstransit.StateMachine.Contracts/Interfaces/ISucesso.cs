namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface ISucesso<TMensagem> where TMensagem : IMensagem
    {
        TMensagem Evento { get; }
    }
}