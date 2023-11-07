using Masstransit.StateMachine.Contracts.Interfaces;
using Masstransit.StateMachine.Contratos.Classes;
using Masstransit.StateMachine.Repositorios;
using Masstransit.StateMachine.Sagas;
using MassTransit;

namespace Masstransit.StateMachine.States
{
    public abstract class StateMachineBase<TEvento> : MassTransitStateMachine<Mensagem> where TEvento : class, IMensagem
    {
        protected StateMachineBase()
        {
            InstanceState(x => x.StatusId, Aguardando, Processando, Falha, Sucesso);

            Event(() => EventoRecebido, x => x.CorrelateById(context => context.Message.Identificador));
            Event(() => FalhaRecebida, x => x.CorrelateById(context => context.Message.Evento.Identificador));
            Event(() => RetentarRecebido, x => x.CorrelateById(context => context.Message.Evento.Identificador));
            Event(() => SucessoRecebido, x => x.CorrelateById(context => context.Message.Evento.Identificador));

            Initially(
                   When(EventoRecebido)
                   .ThenAsync(async (context) =>
                   {
                       context.Saga.SetEventoRecebido(context.Message);

                       await context.Send(Configuracao.UriOrquestrador, context.Message);

                       await context.TransitionToState(Processando);
                   }));

            During(Aguardando,
                   When(EventoRecebido)
                   .TransitionTo(Sucesso));

            During(Aguardando,

                When(FalhaRecebida)
                    .ThenAsync(n => n.Send(Configuracao.UriLogger, n.Message))
                    .TransitionTo(Falha),

                When(EventoRecebido)
                    .TransitionTo(Sucesso));

            During(Processando,

                When(FalhaRecebida)
                    .ThenAsync(n => n.Send(Configuracao.UriLogger, n.Message))
                    .TransitionTo(Falha),

                  When(SucessoRecebido)
                    .ThenAsync(n => n.Send(Configuracao.UriLogger, n.Message))
                    .TransitionTo(Sucesso),

                When(RetentarRecebido)
                    .ThenAsync(async context => await ReprocessarEvento(context)));


            During(Falha,
                 When(FalhaRecebida)
                    .ThenAsync(n => n.Send(Configuracao.UriLogger, n.Message))
                    .TransitionTo(Falha),

                When(RetentarRecebido)
                    .Then(context => context.Send(Configuracao.UriModulo, context.Message.Evento))
                    .TransitionTo(Processando));

        }

        private async Task ReprocessarEvento(BehaviorContext<Mensagem, IRetentar<TEvento>> context)
        {
            var reprocessamentos = RepositorioReprocessamento.Eventos.Where(identificador => identificador == context.Message.Evento.Identificador);

            if (reprocessamentos.Count() < 5)
            {
                var atraso = TimeSpan.FromSeconds(5);

                await context.Send(Configuracao.UriLogger, context.Message);

                await context.ScheduleSend(Configuracao.UriModulo, atraso, context.Message.Evento);
            }
            else
            {
                await context.Send<IErro<TEvento>>(Configuracao.UriSaga, new Falha<TEvento>(context.Message));
            }

        }

        public Event<TEvento>? EventoRecebido { get; private set; }
        public Event<IErro<TEvento>>? FalhaRecebida { get; private set; }
        public Event<ISucesso<TEvento>>? SucessoRecebido { get; private set; }
        public Event<IRetentar<TEvento>>? RetentarRecebido { get; private set; }

        public State? Aguardando { get; private set; }
        public State? Processando { get; private set; }
        public State? Falha { get; private set; }
        public State? Sucesso { get; private set; }
    }
}
