using HSEKAT.Models.Vehicles;

namespace HSEKAT.Services;

public class VehicleType
{
    public string Name { get; }
    public bool HasEnergyUsage { get; }
    public Func<string, int, int, int, double, Vehicle> Create{ get; }

    public VehicleType(string name, bool hasEnergyUsage, Func<string, int, int, int, double, Vehicle> create)
    {
        Name = name;
        HasEnergyUsage = hasEnergyUsage;
        Create = create;
    }
}
