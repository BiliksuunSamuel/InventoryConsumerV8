using InventoryConsumerV8.Options;
using InventoryConsumerV8.Requests;
using InventoryConsumerV8.Services;
using KafkaConsumerHost;
using KafkaConsumerHost.Attributes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace InventoryConsumerV8;

public class InventoryManagerConsumer : KafkaConsumerBase
{
    private readonly ILogger<InventoryManagerConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public InventoryManagerConsumer(ILogger<InventoryManagerConsumer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    [ConsumeTopic(FromType = typeof(IOptions<KafkaExtra>), PropertyName = nameof(KafkaExtra.InventoryCreateTopic))]
    public async Task DoHandleCreateInventory(CreateInventoryRequest message)
    {
        _logger.LogInformation("received message to create inventory\nmessage={message}",
            JsonConvert.SerializeObject(message));

        var service = _serviceProvider.GetRequiredService<IInventoryService>();
        var res = await service.CreateInventoryAsync(message);

        _logger.LogInformation("response from creating inventory\nresponse={response}",
            JsonConvert.SerializeObject(res, Formatting.Indented));


    }

    [ConsumeTopic(FromType = typeof(IOptions<KafkaExtra>), PropertyName = nameof(KafkaExtra.InventoryUpdateTopic))]
    public async Task DoHandleUpdateInventory(InventoryUpdateRequest message)
    {

        _logger.LogInformation("received message to update inventory\nmessage={message}",
            JsonConvert.SerializeObject(message));

        var service = _serviceProvider.GetRequiredService<IInventoryService>();
        var res = await service.UpdateInventoryAsync(message);
        _logger.LogInformation("response from updating inventory\nresponse={response}",
            JsonConvert.SerializeObject(res, Formatting.Indented));
    }
    
    [ConsumeTopic(FromType = typeof(IOptions<KafkaExtra>), PropertyName = nameof(KafkaExtra.InventoryDeleteTopic))]
    public async Task DoHandleDeleteInventory(string id)
    {
        _logger.LogInformation("received message to delete inventory\nid={id}", id);

        var service = _serviceProvider.GetRequiredService<IInventoryService>();
        var res = await service.DeleteInventoryAsync(id);
        _logger.LogInformation("response from deleting inventory\nresponse={response}",
            JsonConvert.SerializeObject(res, Formatting.Indented));
    }
}