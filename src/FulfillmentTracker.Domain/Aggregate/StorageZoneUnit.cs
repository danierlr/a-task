namespace FulfillmentTracker.Domain.Aggregate;

public class StorageZoneUnit {
    public long MaxCount { get; init; }
    public StorageZone Zone { get; init; }

    private readonly Dictionary<OrderId, Order> _orders = new();

    public StorageZoneUnit(StorageZone zone, long maxCount) {
        MaxCount = maxCount;
        Zone = zone;
    }

    public void Place(Order order) {
        if (IsFull) {
            throw new InvalidOperationException($"Storage zone unit is full");
        }

        if (_orders.ContainsKey(order.Id)) {
            throw new InvalidOperationException($"Order with id {order.Id.Value} has been placed already");
        }

        _orders.Add(order.Id, order);
    }

    public Order? Pick(OrderId orderId) {
        Order? order = null;

        _orders.Remove(orderId, out order);

        return order;
    }

    public long Count => _orders.Count;
    public bool IsFull => Count >= MaxCount;
}
