using Masstransit.StateMachine.Contracts.Interfaces;
using Masstransit.StateMachine.Contratos.Classes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Masstransit.StateMachine.Sender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            ConfigureAllServices(services);

            var provider = services.BuildServiceProvider();

            var criarPedido = new CriarPedido()
            {
                Identificador = System.Guid.NewGuid(),
                Numero = 1,
                TimeStamp = System.DateTime.Now,
                Valor = 100
            };

            var bus = provider.GetRequiredService<IBusControl>();

            bus.Publish<ICriarPedido>(criarPedido).Wait();

        }

        private static void ConfigureAllServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", false)
           .Build();

            services.AddMassTransit(massTransit =>
            {
                massTransit.UsingAzureServiceBus((context, configurator) =>
                {
                    configurator.Host(configuration["ServiceBus:ConnectionString"]);

                    configurator.UseServiceBusMessageScheduler();

                    configurator.UseNewtonsoftJsonDeserializer();

                    configurator.UseNewtonsoftJsonSerializer();
                });

            });
        }
    }
}