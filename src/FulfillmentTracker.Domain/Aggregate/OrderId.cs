namespace FulfillmentTracker.Domain.Aggregate;

public readonly record struct OrderId(string Value) {
    public override string ToString() => Value;
}
