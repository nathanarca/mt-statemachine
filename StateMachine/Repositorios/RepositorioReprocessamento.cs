namespace Masstransit.StateMachine.Repositorios
{
    public static class RepositorioReprocessamento
    {
        private static List<Guid> _reprocessado { get; set; }
        public static List<Guid> Eventos 
        {
            get
            {
                if (_reprocessado == null) _reprocessado = new List<Guid>();
                return _reprocessado;
            }
            set
            {
                _reprocessado = value;
            }
        }
    }

    public static class RepositorioFalhas
    {
        private static List<Guid> _falhas { get; set; }
        public static List<Guid> Eventos
        {
            get
            {
                if (_falhas == null) _falhas = new List<Guid>();
                return _falhas;
            }
            set
            {
                _falhas = value;
            }
        }
    }
}
