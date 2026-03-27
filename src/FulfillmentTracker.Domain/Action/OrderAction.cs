using FulfillmentTracker.Domain.KitchenAggregate;
using FulfillmentTracker.Domain.OrderAggregate;

namespace FulfillmentTracker.Domain.Action;

public class OrderAction: IAction {
    public OrderId Id { get; set; }
    public DateTime Time { get; set; }
    //public OrderId Id { get; set; }
    public StorageZone? Target { get; set; }
}
