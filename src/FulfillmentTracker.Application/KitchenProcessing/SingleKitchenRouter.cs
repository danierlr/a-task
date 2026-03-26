using FulfillmentTracker.Application.Shared;

namespace FulfillmentTracker.Application.KitchenProcessing;

public class SingleKitchenRouter : IKitchenRouter {
    private KitchenReactor _kitchenReactor;
    public SingleKitchenRouter(KitchenReactor kitchenReactor) {
        _kitchenReactor = kitchenReactor;
    }

    public void Handle(IKitchenCommand command) {
        _kitchenReactor.Add(command);
    }
}
