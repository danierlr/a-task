using FulfillmentTracker.Domain.Aggregate;

namespace FulfillmentTracker.Domain.Ports;

public interface IStorageStrategy {
    void Place(Order order, DateTime now);
    Order? Pick(OrderId orderId, DateTime now);
}
