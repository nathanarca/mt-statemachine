﻿using Masstransit.StateMachine.Contracts.Interfaces;
using Masstransit.StateMachine.Database;
using Masstransit.StateMachine.Sagas;
using Masstransit.StateMachine.States;
using Masstransit.StateMachine.StatesMachines;
using MassTransit;
using Microsoft.EntityFrameworkCore;
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
                x.SetServiceName("StateMachine");

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
                massTransit.AddStateMachineWithObserverAndSaga<StateCriarPedido, IPedidoCriar>(configuration);
                massTransit.AddStateMachineWithObserverAndSaga<StatePedidoCriado, IPedidoCriado>(configuration);
                massTransit.AddStateMachineWithObserverAndSaga<StatePedidoRemovido, IPedidoRemovido>(configuration);

                massTransit.SetEntityFrameworkSagaRepositoryProvider(options =>
                {
                    options.ConcurrencyMode = ConcurrencyMode.Optimistic;

                    options.AddDbContext<DbContext, EventosDbContext>((provider, builder) =>
                    {
                        builder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                    });
                });

                massTransit.UsingAzureServiceBus((context, configurator) =>
                {
                    configurator.Host(configuration["ServiceBus:ConnectionString"]);

                    configurator.UseServiceBusMessageScheduler();

                    configurator.UseNewtonsoftJsonDeserializer();

                    configurator.UseNewtonsoftJsonSerializer();

                    configurator.ReceiveEndpoint(Configuracao.FilaSaga, configureEndPoint =>
                    {
                        configureEndPoint.UseInMemoryOutbox();

                        configureEndPoint.ConfigureSaga<Mensagem<IPedidoCriar>>(context);
                        configureEndPoint.ConfigureSaga<Mensagem<IPedidoCriado>>(context);
                        configureEndPoint.ConfigureSaga<Mensagem<IPedidoRemovido>>(context);
                    });
                });

            });
        }


    }

    public static class MassTransitLocalExtension
    {
        public static void AddStateMachineWithObserverAndSaga<TStateMachine, TEvento>(this IBusRegistrationConfigurator services, IConfiguration configuration)
           where TStateMachine : StateMachineBase<TEvento>
           where TEvento : class, IMensagem
        {
            services.AddStateObserver<Mensagem<TEvento>, MensagemStateObserver<TEvento>>();

            services.AddSagaStateMachine<TStateMachine, Mensagem<TEvento>>();
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