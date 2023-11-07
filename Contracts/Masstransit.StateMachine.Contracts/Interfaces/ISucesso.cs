namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface ISucesso<out TEvento> where TEvento : IMensagem
    {
        TEvento Evento { get; }
    }
}