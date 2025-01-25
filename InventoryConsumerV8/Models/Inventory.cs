namespace InventoryConsumerV8.Models;

public class Inventory
{
    public string? Id { get; set; }=Guid.NewGuid().ToString("N");
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }
    public string? Category { get; set; }
    public string? ImageUrl { get; set; }
    
}