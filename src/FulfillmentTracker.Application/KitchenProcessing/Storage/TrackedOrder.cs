using FulfillmentTracker.Domain.OrderAggregate;

namespace FulfillmentTracker.Application.KitchenProcessing.Storage;

public record TrackedOrder(
    Order Order,
    DateTime Admitted
);
