using FulfillmentTracker.Domain.Ports;

namespace FulfillmentTracker.Domain.Aggregate;

public class Kitchen {
    private readonly IStorageStrategy _storageStrategy;

    public Kitchen(IStorageStrategy storageStrategy) {
        _storageStrategy = storageStrategy;
    }

    public void PlaceOrder(Order order) {
        _storageStrategy.Place(order);
    }

    public void PickOrder(OrderId orderId) {
        _storageStrategy.Pick(orderId);
    }
}
