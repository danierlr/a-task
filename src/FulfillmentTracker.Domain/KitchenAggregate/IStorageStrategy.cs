using FulfillmentTracker.Domain.OrderAggregate;

namespace FulfillmentTracker.Domain.KitchenAggregate;

public interface IStorageStrategy {
    void Place(Order order, DateTime now);
    Order? Pick(OrderId orderId, DateTime now);
}
