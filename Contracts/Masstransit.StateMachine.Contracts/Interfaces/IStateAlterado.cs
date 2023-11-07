namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface IStateAlterado
    {
        int? PreviousState { get; }
        int CurrentState { get; }
        string? EventType { get; }
        DateTime TimeStamp { get; }
    }
}
