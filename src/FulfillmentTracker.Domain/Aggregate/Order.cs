namespace FulfillmentTracker.Domain.Aggregate;

public class Order {
    public OrderId Id { get; set; }

    public string Name { get; set; }

    public Temperature Temperature { get; set; }

    public decimal Price { get; set; }

    public TimeSpan Freshness { get; set; }
}
