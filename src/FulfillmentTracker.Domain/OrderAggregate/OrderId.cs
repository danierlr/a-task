namespace FulfillmentTracker.Domain.OrderAggregate;

public readonly record struct OrderId(string Value) {
    public override string ToString() => Value;
}
