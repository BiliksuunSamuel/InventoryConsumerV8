namespace InventoryConsumerV8.Requests;

public class CreateInventoryRequest
{
    public int Quantity { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public string? Name { get; set; }
    public string? CreatedBy { get; set; }
    
}