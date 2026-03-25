namespace FulfillmentTracker.Domain.Aggregate;

public class KitchenStorage {
    public StorageZoneUnit Cooler { get; init; }
    public StorageZoneUnit Shelf { get; init; }
    public StorageZoneUnit Heater { get; init; }

    public KitchenStorage(long maxColdCount, long maxShelfCount, long maxHotCount) {
        Cooler = new StorageZoneUnit(StorageZone.Cooler, maxColdCount);
        Shelf = new StorageZoneUnit(StorageZone.Shelf, maxShelfCount);
        Heater = new StorageZoneUnit(StorageZone.Heater, maxHotCount);
    }
}
