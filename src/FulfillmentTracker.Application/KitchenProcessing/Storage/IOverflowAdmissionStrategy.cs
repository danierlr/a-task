using FulfillmentTracker.Domain.OrderAggregate;

namespace FulfillmentTracker.Application.KitchenProcessing.Storage;

public interface IOverflowAdmissionStrategy {
    Order? FindOrderToEvict(Order admitted);
}
