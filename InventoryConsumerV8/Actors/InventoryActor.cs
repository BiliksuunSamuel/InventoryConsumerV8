using AkkaNetApiAdapter;
using InventoryConsumerV8.Requests;
using InventoryConsumerV8.Services;
using Newtonsoft.Json;

namespace InventoryConsumerV8.Actors;

public class InventoryActor:BaseActor
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<InventoryActor> _logger;


    public InventoryActor(IServiceProvider serviceProvider, ILogger<InventoryActor> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        
        //Handle Events
        ReceiveAsync<InventoryUpdateRequest>(DoHandleInventoryUpdate);
    }


    private async Task DoHandleInventoryUpdate(InventoryUpdateRequest message)
    {
        try
        {
            _logger.LogInformation("Received an inventory update request: {Data}",
                JsonConvert.SerializeObject(message, Formatting.Indented));

            using var scope = _serviceProvider.CreateScope();
            var inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryService>();
            var result = await inventoryService.UpdateInventoryAsync(message);
            
            _logger.LogInformation("Inventory update request handled successfully: {Data}",
                JsonConvert.SerializeObject(result, Formatting.Indented));

            Sender.Tell(result, Self);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error occurred while handling inventory update\n{Data}",
                JsonConvert.SerializeObject(message, Formatting.Indented));
            Sender.Tell(null, Self);
        }
    }
}