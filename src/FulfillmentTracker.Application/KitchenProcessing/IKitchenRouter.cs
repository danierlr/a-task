using FulfillmentTracker.Application.Shared;

namespace FulfillmentTracker.Application.KitchenProcessing;

public interface IKitchenRouter {
    void Handle(IKitchenCommand command);
}
