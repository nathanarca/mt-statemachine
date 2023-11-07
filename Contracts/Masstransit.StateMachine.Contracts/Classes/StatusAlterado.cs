using Masstransit.StateMachine.Contracts.Interfaces;

namespace Masstransit.StateMachine.Contracts.Classes
{
    public class StateAlterado : IStateAlterado
    {
        public StateAlterado(string? contrato, DateTime dataHora, int previousState, int currentState)
        {
            EventType = contrato;
            TimeStamp = dataHora;
            PreviousState = previousState;
            CurrentState = currentState;
        }

        public int? PreviousState { get; set; }

        public int CurrentState { get; set; }

        public string? EventType { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
