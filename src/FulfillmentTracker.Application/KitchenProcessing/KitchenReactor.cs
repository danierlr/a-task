using FulfillmentTracker.Application.KitchenProcessing.Commands;
using FulfillmentTracker.Application.Shared;
using FulfillmentTracker.Domain.Aggregate;
using System.Threading.Channels;

namespace FulfillmentTracker.Application.KitchenProcessing;

public class KitchenReactor {
    private Kitchen _kitchen;
    private KitchenStorage _kitchenStorage;

    private Channel<Command> _channel;

    public KitchenReactor() {
        _channel = Channel.CreateUnbounded<Command>(new UnboundedChannelOptions() {
            SingleReader = true,
            SingleWriter = false,
        });

        _kitchenStorage = new KitchenStorage(6, 12, 6); // TODO take params from config

        var strategy = new InterpolatedPriorityStrategy();

        _kitchen = new Kitchen(strategy);
    }

    public void PlaceOrder(PlaceOrderCommand placeCommand) {
        // TODO
    }

    public void PickOrder(PickOrderCommand pickCommand) {
        // TODO
    }

    public async Task Run(CancellationToken cancellationToken) {
        // TODO
    }
}
