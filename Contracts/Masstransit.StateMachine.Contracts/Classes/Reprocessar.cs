using Masstransit.StateMachine.Contracts.Interfaces;

namespace Masstransit.StateMachine.Contratos.Classes
{
    public class Reprocessar<TEvento> : IRetentar<TEvento> where TEvento : IMensagem
    {
        public Reprocessar(TEvento evento)
        {
            TimeStamp = DateTime.Now;
            this.Evento = evento;
        }

        public TEvento Evento { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
