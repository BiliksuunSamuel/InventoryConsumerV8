namespace InventoryConsumerV8.Options;

public class KafkaExtra
{
    public string? InventoryUpdateTopic { get; set; }
    public string? InventoryCreateTopic { get; set; }
    public string? InventoryDeleteTopic { get; set; }
}