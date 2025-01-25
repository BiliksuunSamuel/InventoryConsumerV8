using InventoryConsumerV8.Options;
using InventoryConsumerV8.Requests;
using KafkaConsumerHost;
using KafkaConsumerHost.Attributes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace InventoryConsumerV8;

public class InventoryManagerConsumer : KafkaConsumerBase
{
    private readonly ILogger<InventoryManagerConsumer> _logger;

    public InventoryManagerConsumer(ILogger<InventoryManagerConsumer> logger)
    {
        _logger = logger;
    }

    [ConsumeTopic(FromType = typeof(IOptions<KafkaExtra>), PropertyName = nameof(KafkaExtra.InventoryCreateTopic))]
    public async Task DoHandleCreateInventory(CreateInventoryRequest message)
    {
        await Task.CompletedTask;
        _logger.LogInformation("received message to create inventory\nmessage={message}",
            JsonConvert.SerializeObject(message));
    }

    [ConsumeTopic(FromType = typeof(IOptions<KafkaExtra>), PropertyName = nameof(KafkaExtra.InventoryUpdateTopic))]
    public async Task DoHandleUpdateInventory(InventoryUpdateRequest message)
    {
        
        await Task.CompletedTask;
        _logger.LogInformation("received message to update inventory\nmessage={message}",
            JsonConvert.SerializeObject(message));
    }
}