namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface IRetentar<out TEvento> where TEvento : IMensagem
    {
        TEvento Evento { get;}

        DateTime TimeStamp { get; }
    }
}