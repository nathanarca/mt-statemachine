using Masstransit.StateMachine.Contracts.Interfaces;
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
                x.SetServiceName("ConsumerPedido");

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

            services.AddMassTransit(massTransit =>
            {
                massTransit.AddConsumer<ConsumerPedido<IPedidoCriado>>();
                massTransit.AddConsumer<ConsumerPedido<IPedidoCriar>>();
                massTransit.AddConsumer<ConsumerPedido<IPedidoRemovido>>();

                massTransit.UsingAzureServiceBus((context, configurator) =>
                {
                    configurator.Host(configuration["ServiceBus:ConnectionString"]);

                    configurator.UseNewtonsoftJsonDeserializer();

                    configurator.UseNewtonsoftJsonSerializer();

                    configurator.ReceiveEndpoint("pedido_criado", configureEndPoint =>
                    {
                        configureEndPoint.ConfigureConsumer<ConsumerPedido<IPedidoCriado>>(context);
                    });

                    configurator.ReceiveEndpoint("pedido_criar", configureEndPoint =>
                    {
                        configureEndPoint.ConfigureConsumer<ConsumerPedido<IPedidoCriar>>(context);
                    });

                    configurator.ReceiveEndpoint("pedido_removido", configureEndPoint =>
                    {
                        configureEndPoint.ConfigureConsumer<ConsumerPedido<IPedidoRemovido>>(context);
                    });

                    configurator.ConfigureEndpoints(context);

                    configurator.UseServiceBusMessageScheduler();
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