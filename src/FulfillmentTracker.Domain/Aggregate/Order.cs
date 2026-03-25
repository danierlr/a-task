namespace FulfillmentTracker.Domain.Aggregate;

public class Order {
    public OrderId Id { get; init; }

    public string Name { get; init; }

    public Temperature Temperature { get; init; }

    public decimal Price { get; init; }

    public TimeSpan Freshness { get; init; }
}
