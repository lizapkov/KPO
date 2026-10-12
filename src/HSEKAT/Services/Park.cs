using HSEKAT.Interfaces;
using HSEKAT.Models;
using HSEKAT.Models.Items;
using HSEKAT.Models.Vehicles;

namespace HSEKAT.Services;

public class Park
{
    public const int MinScoreForNovice = 6;

    private readonly IServiceCenter _serviceCenter;
    private readonly InventoryNumberGenerator _invNumGenerator;
    private readonly List<Vehicle> _vehicles = new List<Vehicle>();
    private readonly List<Item> _items = new List<Item>();

    public Park(IServiceCenter serviceCenter, InventoryNumberGenerator invNumGenerator)
    {
        _serviceCenter = serviceCenter;
        _invNumGenerator = invNumGenerator;
    }

    public int VehicleCount => _vehicles.Count;
    public int ItemCount => _items.Count;

    public InspectionRes AddVehicle(Vehicle vehicle)
    {
        if (vehicle == null)
        {
            throw new ArgumentNullException(nameof(vehicle));
        }

        InspectionRes result = _serviceCenter.Inspect(vehicle);

        if (result.Approval)
        {
            vehicle.InventoryNumber = _invNumGenerator.NextNumber();
            _vehicles.Add(vehicle);
        }

        return result;
    }

    public void AddItem(Item item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        item.InventoryNumber = _invNumGenerator.NextNumber();
        _items.Add(item);
    }

    public double GetTotalEnergy()
    {
        double result = 0;

        foreach (IInventoryItem item in GetAllStuff())
        {
            if (item is IEnergyUser user)
            {
                result += user.DailyEnergyUsage;
            }
        }

        return result;
    }

    public List<Vehicle> GetVehiclesForNovice()
    {
        List<Vehicle> result = new List<Vehicle>();

        foreach (Vehicle vehicle in _vehicles)
        {
            if (vehicle.Score >= MinScoreForNovice)
            {
                result.Add(vehicle);
            }
        }

        return result;
    }

    public List<IInventoryItem> GetAllStuff()
    {
        List<IInventoryItem> result = new List<IInventoryItem>();

        foreach (Vehicle vehicle in _vehicles)
        {
            result.Add(vehicle);
        }

        foreach (Item item in _items)
        {
            result.Add(item);
        }

        return result;
    }
}
