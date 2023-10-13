using MovieStoreWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHostedService<TimedHostedService>();
    })
    .Build();

host.Run();
