using FulfillmentTracker.Domain.Action;
using FulfillmentTracker.Domain.KitchenAggregate;

namespace FulfillmentTracker.Domain.OrderAggregate;

public class Order {
    private readonly IActionLedger _actionLedger;

    public Order(IActionLedger actionLedger) {
        _actionLedger = actionLedger;
    }

    public OrderId Id { get; init; }

    public string Name { get; init; } = "";

    public Temperature Temperature { get; init; }

    public decimal Price { get; init; }

    public TimeSpan Freshness { get; init; }

    public void MarkAsPlaced(StorageZone target, DateTime moment) {
        OrderAction action = new OrderAction(Id, moment, OrderActionVariant.Place, target);

        _actionLedger.Record(action);
    }

    public void MarkAsMoved(StorageZone target, DateTime moment) {
        OrderAction action = new OrderAction(Id, moment, OrderActionVariant.Move, target);

        _actionLedger.Record(action);
    }

    public void MarkAsPicked(DateTime moment) {
        OrderAction action = new OrderAction(Id, moment, OrderActionVariant.Pickup, null);

        _actionLedger.Record(action);
    }

    public void MarkAsDiscarded(DateTime moment) {
        OrderAction action = new OrderAction(Id, moment, OrderActionVariant.Discard, null);

        _actionLedger.Record(action);
    }
}
