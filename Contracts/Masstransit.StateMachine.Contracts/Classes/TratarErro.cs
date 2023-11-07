using Masstransit.StateMachine.Contracts.Interfaces;

namespace Masstransit.StateMachine.Contratos.Classes
{
    public class TratarErro<TEvento> : ITratamento<TEvento> where TEvento : IMensagem
    {
        public TratarErro(TEvento message)
        {
            TimeStamp = DateTime.Now;
            this.Evento = message;
        }

        public TEvento Evento { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
