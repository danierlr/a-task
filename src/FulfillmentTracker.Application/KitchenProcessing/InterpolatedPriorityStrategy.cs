using FulfillmentTracker.Domain.Aggregate;
using FulfillmentTracker.Domain.Ports;

namespace FulfillmentTracker.Application.KitchenProcessing;

internal class InterpolatedPriorityStrategy : IStorageStrategy {
    public Order PickOrder(OrderId orderId) {
        throw new NotImplementedException();
    }

    public void PlaceOrder(Order order) {
        throw new NotImplementedException();
    }
}
