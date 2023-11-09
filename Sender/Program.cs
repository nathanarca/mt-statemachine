using Masstransit.StateMachine.Contracts.Interfaces;
using Masstransit.StateMachine.Contratos.Classes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Masstransit.StateMachine.Sender
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            ConfigureAllServices(services);

            var provider = services.BuildServiceProvider();

            var criarPedido = new CriarPedido()
            {
                Identificador = new Guid("09BE0BDC-43CD-4AC6-8DB8-F6BB3F2D4569"),
                Numero = 1,
                TimeStamp = System.DateTime.Now,
                Valor = 100
            };

            var message = new Sucesso<ICriarPedido>(criarPedido);

            var busControl = provider.GetRequiredService<IBusControl>();

            await busControl.Publish<ISucesso<ICriarPedido>>(message);
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