using InventoryConsumerV8.Models;
using InventoryConsumerV8.Requests;
using Mapster;
using MongoODM.Net;
using Newtonsoft.Json;
// ReSharper disable All

namespace InventoryConsumerV8.Services;

public class InventoryService:IInventoryService
{
    private readonly ILogger<InventoryService> _logger;
    private readonly MongoRepository<Inventory> _inventoryRepository;
    public InventoryService(ILogger<InventoryService> logger, MongoRepository<Inventory> inventoryRepository)
    {
        _logger = logger;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Inventory?> GetInventoryAsync(string id)
    {
        try
        {
            var res = await _inventoryRepository.GetByIdAsync(id);
            return res;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error occurred while getting inventory");
            return null;
        }
    }

    public async Task<Inventory?> CreateInventoryAsync(CreateInventoryRequest inventory)
    {
        try
        {
            var newInventory = inventory.Adapt<Inventory>();
            await _inventoryRepository.AddAsync(newInventory);
            return newInventory;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error occurred while creating inventory");
            return null;
        }
    }

    public async Task<Inventory?> UpdateInventoryAsync(InventoryUpdateRequest inventory)
    {
        try
        {
            _logger.LogInformation("updating inventory\n{Data}",
                JsonConvert.SerializeObject(inventory, Formatting.Indented));
            var existingInventory = await _inventoryRepository.GetByIdAsync(inventory.Id!);
            if (existingInventory == null)
            {
                _logger.LogError("inventory not found\n{Data}",
                    JsonConvert.SerializeObject(inventory, Formatting.Indented));
                return null;
            }

            existingInventory = inventory.Adapt(existingInventory);
            existingInventory.UpdatedAt=DateTime.UtcNow;
            await _inventoryRepository.UpdateAsync(existingInventory.Id!, existingInventory);
            return existingInventory;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error occurred while updating inventory\n{Data}",
                JsonConvert.SerializeObject(inventory, Formatting.Indented));
            return null;
        }
    }

    public async Task<Inventory?> DeleteInventoryAsync(string id)
    {
        try
        {
            var inventory=await _inventoryRepository.GetByIdAsync(id);
            if (inventory == null)
            {
                _logger.LogError("inventory not found\n{Id}", id);
                return null;
            }
            var res = await _inventoryRepository.DeleteByIdAsync(id);
            if (res == 0)
            {
                _logger.LogError("inventory not found\n{Id}", id);
                return null;
            }
            return inventory;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error occurred while deleting inventory\n{Id}", id);
            return null;
        }
    }
}