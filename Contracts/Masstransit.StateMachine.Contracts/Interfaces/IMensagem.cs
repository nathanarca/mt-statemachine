namespace Masstransit.StateMachine.Contracts.Interfaces
{
    public interface IMensagem
    {
        Guid Identificador { get; }

        DateTime TimeStamp { get; }
    }


}
