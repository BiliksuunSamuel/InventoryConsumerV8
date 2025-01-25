
using InventoryConsumerV8.Models;
using InventoryConsumerV8.Options;
using InventoryConsumerV8.Services;
using KafkaConsumerHost.Extensions;
using KafkaConsumerHost.Options;
using MongoODM.Net.Extensions;
using MongoODM.Net.Options;

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
        
        //
        services.AddMongoDbContext(configuration);
        services.AddMongoRepository<Inventory>("ShoppeDb");

        services.AddScoped<IInventoryService, InventoryService>();
    });
