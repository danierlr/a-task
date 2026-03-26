namespace FulfillmentTracker.Domain.Aggregate;

public class KitchenStorage {
    public KitchenStorage(long maxColdCount, long maxShelfCount, long maxHotCount) {
        StorageZoneUnit Cooler = new StorageZoneUnit(StorageZone.Cooler, maxColdCount);
        StorageZoneUnit Shelf = new StorageZoneUnit(StorageZone.Shelf, maxShelfCount);
        StorageZoneUnit Heater = new StorageZoneUnit(StorageZone.Heater, maxHotCount);

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
