namespace FulfillmentTracker.Domain.Action;

public interface IActionLedger {
    void Record(IAction action);
}
