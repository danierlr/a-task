using FulfillmentTracker.Domain.KitchenAggregate;
using FulfillmentTracker.Domain.OrderAggregate;

namespace FulfillmentTracker.Domain.Action;

public record OrderAction(
    OrderId Id,
    DateTime Time,
    OrderActionVariant Variant,
    StorageZone? Target
) : IAction;
