using FulfillmentTracker.Application.Shared;
using FulfillmentTracker.Domain.Aggregate;
using FulfillmentTracker.Domain.Ports;

namespace FulfillmentTracker.Application.KitchenProcessing.Storage;

internal class StorageStrategy : IStorageStrategy {
    private readonly KitchenStorage _kitchenStorage;
    private readonly IOverflowAdmissionStrategy _overflowAdmissionStrategy;

    //private readonly IIndexedHeap<Order, DateTime> _ordersByExpiration = new IndexedHeapBinary<Order, DateTime>();

    private readonly IIndexedHeap<Order, decimal> _ordersSuboptimalColdByPrice;
    private readonly IIndexedHeap<Order, decimal> _ordersSuboptimalHotByPrice;

    public StorageStrategy(KitchenStorage kitchenStorage, IOverflowAdmissionStrategy overflowAdmissionStrategy) {
        _kitchenStorage = kitchenStorage;
        _overflowAdmissionStrategy = overflowAdmissionStrategy;

        var maxPriceComparer = Comparer<decimal>.Create((a, b) => b.CompareTo(a));
        _ordersSuboptimalColdByPrice = new IndexedHeapBinary<Order, decimal>(maxPriceComparer);
        _ordersSuboptimalHotByPrice = new IndexedHeapBinary<Order, decimal>(maxPriceComparer);
    }

    public Order? Pick(OrderId orderId, DateTime now) {
        throw new NotImplementedException();
    }

    public void Place(Order order, DateTime now) {
        StorageZoneUnit optimalStorage = _kitchenStorage.GetStorageOptimal(order.Temperature);

        if (!optimalStorage.IsFull) {
            optimalStorage.Place(order);
            order.MarkAsPlaced(optimalStorage.Zone, now);
            return;
        }

        StorageZoneUnit cooler = _kitchenStorage.Cooler;
        StorageZoneUnit shelf = _kitchenStorage.Shelf;
        StorageZoneUnit heater = _kitchenStorage.Heater;

        // TODO handle expired orders

        if (shelf.IsFull) {
            // If room temp is suboptimal for new order, we still try to move existing suboptimal temp order to the optimal - according to the task description

            // we choose the order to move to optimal temp my max price for now // TODO? refine this choice

            bool shouldMoveCold = _ordersSuboptimalColdByPrice.Count > 0 && !cooler.IsFull;
            bool shouldMoveHot = _ordersSuboptimalHotByPrice.Count > 0 && !heater.IsFull;

            if (shouldMoveCold && shouldMoveHot) {
                Order cold = _ordersSuboptimalColdByPrice.Peek();
                Order hot = _ordersSuboptimalHotByPrice.Peek();

                if (cold.Price > hot.Price) {
                    shouldMoveHot = false;
                } else {
                    shouldMoveCold = false;
                }
            }

            if (shouldMoveCold) {
                Order moved = _ordersSuboptimalColdByPrice.Dequeue();
                shelf.Pick(moved.Id);
                cooler.Place(moved);
                moved.MarkAsMoved(StorageZone.Cooler, now);
            }

            if (shouldMoveHot) {
                Order moved = _ordersSuboptimalHotByPrice.Dequeue();
                shelf.Pick(moved.Id);
                heater.Place(moved);
                moved.MarkAsMoved(StorageZone.Heater, now);
            }
        }

        if (shelf.IsFull) {
            Order? evicted = _overflowAdmissionStrategy.FindOrderToEvict(order);

            if (evicted is not null) {
                shelf.Pick(evicted.Id);
                evicted.MarkAsDiscarded(now);
            }
        }

        if(shelf.IsFull) {
            order.MarkAsDiscarded(now);
            return;
        }

        shelf.Place(order);
        order.MarkAsPlaced(StorageZone.Shelf, now);

        if(order.Temperature == Temperature.Cold) {
            _ordersSuboptimalColdByPrice.Enqueue(order, order.Price);
        }

        if (order.Temperature == Temperature.Hot) {
            _ordersSuboptimalHotByPrice.Enqueue(order, order.Price);
        }
    }
}
