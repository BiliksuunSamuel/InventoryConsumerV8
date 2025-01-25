using InventoryConsumerV8.Models;
using InventoryConsumerV8.Requests;

namespace InventoryConsumerV8.Services;

public interface IInventoryService
{
    Task<Inventory?> GetInventoryAsync(string id);
    Task<Inventory?> CreateInventoryAsync(CreateInventoryRequest inventory);
    Task<Inventory?> UpdateInventoryAsync(InventoryUpdateRequest inventory);
    Task<Inventory?> DeleteInventoryAsync(string id);
    
}