namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface IErro<out TEvento> where TEvento : IMensagem
    {
        TEvento Evento { get; }
    }
}