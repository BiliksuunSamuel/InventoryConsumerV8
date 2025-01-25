namespace InventoryConsumerV8.Requests;

public class InventoryUpdateRequest
{
    public int Quantity { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public string? Name { get; set; }
    public string? UpdatedBy { get; set; }
}