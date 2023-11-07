namespace Masstransit.StateMachine
{
    public static class Configuracao
    {
        public static readonly string FilaModulo = "modulo";
        public static readonly string FilaOrquestrador = "orquestrador";
        public static readonly string FilaLogger = "logger";
        public static readonly string FilaSaga = "saga";

        public static readonly Uri UriModulo = new Uri($"queue:{FilaModulo}");
        public static readonly Uri UriOrquestrador = new Uri($"queue:{FilaOrquestrador}");
        public static readonly Uri UriLogger = new Uri($"queue:{FilaLogger}");
        public static readonly Uri UriSaga = new Uri($"queue:{FilaSaga}");

    }
}
