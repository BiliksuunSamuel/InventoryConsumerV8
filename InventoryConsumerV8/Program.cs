
using Akka.Actor;
using AkkaNetApiAdapter.Extensions;
using AkkaNetApiAdapter.Options;
using InventoryConsumerV8.Actors;
using InventoryConsumerV8.Models;
using InventoryConsumerV8.Options;
using InventoryConsumerV8.Requests;
using InventoryConsumerV8.Services;
using KafkaConsumerHost.Extensions;
using KafkaConsumerHost.Options;
using MongoODM.Net.Extensions;

var host = CreateHostBuilder(args).Build();
ResolveActorSystem(host);
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
        
        //
        services.AddActorSystem(c => configuration.GetSection(nameof(ActorConfig)).Bind(c),
            actorTypes: new[] { typeof(InventoryActor) },
            subscriptions: new[] { (typeof(InventoryActor), typeof(InventoryUpdateRequest)) });
    });


static IHost ResolveActorSystem(IHost host)
{
    var actorSystem = host.Services.GetRequiredService<ActorSystem>();
    _ = actorSystem ?? throw new ArgumentNullException(nameof(actorSystem));
    return host;
}
