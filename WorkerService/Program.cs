using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<FileWatcherService>();
        services.AddTransient<EmailService.IEmailService, EmailService.EmailService>();
    })
    .Build();

host.Run();
