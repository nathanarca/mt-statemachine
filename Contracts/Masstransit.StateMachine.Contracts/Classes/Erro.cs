using Masstransit.StateMachine.Contracts.Interfaces;

namespace Masstransit.StateMachine.Contratos.Classes
{
    public class Erro<TEvento> : IErro<TEvento> where TEvento : IMensagem
    {
        public Erro(IRetentar<TEvento> message)
        {
            TimeStamp = DateTime.Now;
            this.Evento = message.Evento;
        }

        public TEvento Evento {get;set;}

        public DateTime TimeStamp { get; set; }
    }
}
