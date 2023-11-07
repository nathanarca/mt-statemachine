namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface ITratamento<out TEvento> where TEvento : IMensagem
    {
        TEvento Evento { get; }

        DateTime TimeStamp { get; }
    }
}