namespace FulfillmentTracker.Domain.Aggregate;

public class Order {
    public OrderId Id { get; init; }

    public string Name { get; init; }

    public Temperature Temperature { get; init; }

    public decimal Price { get; init; }

    public TimeSpan Freshness { get; init; }

    public void MarkAsPlaced(StorageZone target, DateTime moment) {
        throw new NotImplementedException();
    }

    public void MarkAsMoved(StorageZone target, DateTime moment) {
        throw new NotImplementedException();
    }

    public void MarkAsPicked(DateTime moment) {
        throw new NotImplementedException();
    }

    public void MarkAsDiscarded(DateTime moment) {
        throw new NotImplementedException();
    }
}
