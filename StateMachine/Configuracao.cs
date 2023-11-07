namespace Masstransit.StateMachine
{
    public static class Configuracao
    {
        public static readonly Uri FilaModulo = new Uri("queue:modulo");
        public static readonly Uri FilaOrquestrador = new Uri("queue:orquestrador");
        public static readonly Uri FilaLogger = new Uri("queue:logger");
        public static readonly Uri FilaSaga = new Uri("queue:saga");

    }
}
