using Masstransit.StateMachine.Contracts.Enumns;
using Masstransit.StateMachine.Contracts.Interfaces;

namespace Masstransit.StateMachine.Contracts.Classes
{
    public class StateAlterado : IStateAlterado
    {
        public StateAlterado(int? tipoMensagem, DateTime dataHora, int previousState, int currentState)
        {
            EventType = tipoMensagem;
            TimeStamp = dataHora;
            PreviousState = previousState;
            CurrentState = currentState;
        }

        public int? PreviousState { get; set; }

        public int CurrentState { get; set; }

        public int? EventType { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
