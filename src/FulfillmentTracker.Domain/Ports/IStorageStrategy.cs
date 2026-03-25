using FulfillmentTracker.Domain.Aggregate;

namespace FulfillmentTracker.Domain.Ports;

public interface IStorageStrategy {
    void Place(Order order);
    Order? Pick(OrderId orderId);
}
