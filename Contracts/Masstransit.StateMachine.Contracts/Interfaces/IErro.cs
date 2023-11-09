namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface IErro<TEvento> where TEvento : IMensagem
    {
        TEvento Evento { get; }
    }
}