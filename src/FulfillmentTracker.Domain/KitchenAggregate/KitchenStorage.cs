using FulfillmentTracker.Domain.OrderAggregate;

namespace FulfillmentTracker.Domain.KitchenAggregate;

public class KitchenStorage {
    public StorageZoneUnit Cooler { get; init; }
    public StorageZoneUnit Shelf { get; init; }
    public StorageZoneUnit Heater { get; init; }

    public KitchenStorage(long maxColdCount, long maxShelfCount, long maxHotCount) {
        Cooler = new StorageZoneUnit(StorageZone.Cooler, maxColdCount);
        Shelf = new StorageZoneUnit(StorageZone.Shelf, maxShelfCount);
        Heater = new StorageZoneUnit(StorageZone.Heater, maxHotCount);

        _zoneUnits = new();
        _optimalZoneUnits = new();

        _zoneUnits[StorageZone.Cooler] = Cooler;
        _zoneUnits[StorageZone.Shelf] = Shelf;
        _zoneUnits[StorageZone.Heater] = Heater;

        _optimalZoneUnits[Temperature.Cold] = Cooler;
        _optimalZoneUnits[Temperature.Room] = Shelf;
        _optimalZoneUnits[Temperature.Hot] = Heater;
    }

    private readonly Dictionary<StorageZone, StorageZoneUnit> _zoneUnits;
    private readonly Dictionary<Temperature, StorageZoneUnit> _optimalZoneUnits;

    public StorageZoneUnit GetStorage(StorageZone zone) => _zoneUnits[zone];
    public StorageZoneUnit GetStorageOptimal(Temperature temperature) => _optimalZoneUnits[temperature];
}
