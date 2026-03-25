using FulfillmentTracker.Domain.Aggregate;

namespace FulfillmentTracker.Domain.Ports;

public interface IStorageStrategy {
    void PlaceOrder(Order order);
    Order? PickOrder(OrderId orderId);
}
