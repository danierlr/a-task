using FulfillmentTracker.Application.KitchenProcessing.Commands;
using FulfillmentTracker.Application.Shared;
using FulfillmentTracker.Domain.Aggregate;
using System.Threading.Channels;

namespace FulfillmentTracker.Application.KitchenProcessing;

// If we need the reactor to be more configurable, we move composition up, to where it is needed

// TODO change into factory pattern and better

public class KitchenReactor {
    private Kitchen _kitchen;
    private KitchenStorage _kitchenStorage;

    private Channel<IKitchenCommand> _channel;

    public KitchenReactor() {
        _channel = Channel.CreateUnbounded<IKitchenCommand>(new UnboundedChannelOptions() {
            SingleReader = true,
            SingleWriter = false,
        });

        _kitchenStorage = new KitchenStorage(6, 12, 6); // TODO take params from config

        var strategy = new StorageStrategy();

        _kitchen = new Kitchen(strategy);
    }

    public void Add(IKitchenCommand command) {
        bool added = _channel.Writer.TryWrite(command);

        if (!added) {
            throw new InvalidOperationException("Unbound channel rejected the write");
        }
    }

    private void Handle(IKitchenCommand command) {

        switch (command) {
            case PlaceOrderCommand placeCommand: PlaceOrder(placeCommand); break;
            case PickOrderCommand pickCommand: PickOrder(pickCommand); break;
            default: throw new InvalidOperationException($"Unexpected command: {command.GetType().Name}");
        }
    }

    private void PlaceOrder(PlaceOrderCommand placeCommand) {
        //_kitchen.PlaceOrder()
    }

    private void PickOrder(PickOrderCommand pickCommand) {
        // TODO
    }

    private TimeSpan Tick() {
        // TODO

        return TimeSpan.Zero;
    }

    public async Task Run(CancellationToken cancellationToken) {
        // TODO
    }
}
