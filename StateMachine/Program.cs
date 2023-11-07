using Masstransit.StateMachine.Sagas;
using Masstransit.StateMachine.States;
using Masstransit.StateMachine.StatesMachines;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Topshelf;


namespace Masstransit.StateMachine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            ConfigureAllServices(services);

            var provider = services.BuildServiceProvider();

            HostFactory.Run(x =>
            {
                x.Service<ServiceConnector>(s =>
                {
                    s.ConstructUsing(() => new ServiceConnector(provider));
                    s.WhenStarted(async service => await service.Start());
                    s.WhenStopped(async service => await service.Stop());
                });
            });
        }

        private static void ConfigureAllServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", false)
           .Build();

            services.AddTransient<SagaStateMachineInstance, Mensagem>();

            services.AddMassTransit(massTransit =>
            {
                massTransit.AddStateObserver<Mensagem, MensagemStateObserver>();

                massTransit.AddSagaStateMachines(typeof(StateCriarPedido).Assembly);

                massTransit.SetInMemorySagaRepositoryProvider();

                massTransit.UsingAzureServiceBus((context, configurator) =>
                {
                    configurator.Host(configuration["ServiceBus:ConnectionString"]);

                    configurator.UseServiceBusMessageScheduler();

                    configurator.UseNewtonsoftJsonDeserializer();

                    configurator.UseNewtonsoftJsonSerializer();

                    configurator.ReceiveEndpoint(Configuracao.FilaSaga, configureEndPoint =>
                    {
                        configureEndPoint.UseInMemoryOutbox();

                        configureEndPoint.ConfigureSaga<Mensagem>(context);

                        configureEndPoint.StateMachineSaga(context.GetService<StateCriarPedido>(), context);
                        configureEndPoint.StateMachineSaga(context.GetService<StatePedidoCriado>(), context);

                    });

                    configurator.ConfigureEndpoints(context);

                });

            });
        }
    }

    public class ServiceConnector
    {
        private readonly IServiceProvider _provider;

        public ServiceConnector(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Start()
        {
            await Task.Run(async () =>
            {
                Console.WriteLine($"[{DateTime.Now}] Iniciando serviço");
                var bus = _provider.GetRequiredService<IBusControl>();
                await bus.StartAsync();
                Console.WriteLine($"[{DateTime.Now}] Serviço iniciado");
            });
        }

        public async Task Stop()
        {
            await _provider.GetRequiredService<IBusControl>().StopAsync();
        }
    }
}