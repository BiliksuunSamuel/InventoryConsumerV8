
using InventoryConsumerV8.Options;
using KafkaConsumerHost.Extensions;
using KafkaConsumerHost.Options;

var host = CreateHostBuilder(args).Build();
await host.RunAsync();
return;

//
IHostBuilder CreateHostBuilder(string[]args)=>Host
    .CreateDefaultBuilder(args)
    .AddKafkaConsumerHost()
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        services.Configure<KafkaConsumerConfig>(c=>configuration.GetSection(nameof(KafkaConsumerConfig)).Bind(c));
        services.Configure<KafkaExtra>(c => configuration.GetSection(nameof(KafkaExtra)).Bind(c));

        services.AddLogging(logging =>
        {
            logging.AddConsole();
            logging.AddDebug();
        });
    });
