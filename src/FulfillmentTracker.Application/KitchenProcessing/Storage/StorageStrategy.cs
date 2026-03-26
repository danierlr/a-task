using FulfillmentTracker.Domain.Aggregate;
using FulfillmentTracker.Domain.Ports;

namespace FulfillmentTracker.Application.KitchenProcessing.Storage;

internal class StorageStrategy : IStorageStrategy {
    private readonly KitchenStorage _kitchenStorage;

    public StorageStrategy(KitchenStorage kitchenStorage) {
        _kitchenStorage = kitchenStorage;
    }

    public Order? Pick(OrderId orderId) {
        throw new NotImplementedException();
    }

    public void Place(Order order) {
        StorageZoneUnit optimalStorage = _kitchenStorage.GetStorageOptimal(order.Temperature);

        if(optimalStorage.IsFull) {
            if(optimalStorage.Zone == StorageZone.Shelf) {

            } else {
                //StorageZone shelfStorage = 
            }
        }

        throw new NotImplementedException();
    }
}
