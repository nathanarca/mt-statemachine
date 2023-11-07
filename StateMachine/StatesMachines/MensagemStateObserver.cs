using Masstransit.StateMachine.Contracts.Classes;
using Masstransit.StateMachine.Sagas;
using MassTransit;

namespace Masstransit.StateMachine.StatesMachines
{
    internal class MensagemStateObserver : IStateObserver<Mensagem>
    {
        public async Task StateChanged(BehaviorContext<Mensagem> context, State currentState, State previousState)
        {
            if (previousState == null || currentState == null) return;

            var status = new StateAlterado(context.Saga.Contrato, context.Saga.DataHora, ToStatusId(previousState), ToStatusId(currentState));

            await context.Send(Configuracao.UriLogger, status);
        }

        private static int ToStatusId(State state)
        {
            switch (state.Name)
            {
                case States.Initial:
                    return 1;
                case States.Final:
                    return 2;
                case States.Aguardando:
                    return 3;
                case States.Processando:
                    return 4;
                case States.Falha:
                    return 5;
                case States.Sucesso:
                    return 6;
            }

            throw new InvalidOperationException("Status não encontrado");
        }
    }

    public static class States
    {
        public const string Initial = "Initial";
        public const string Final = "Final";
        public const string Sucesso = "Sucesso";
        public const string Aguardando = "Aguardando";
        public const string Processando = "Processando";
        public const string Falha = "Falha";
    }
}
