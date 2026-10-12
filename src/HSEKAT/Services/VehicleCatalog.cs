using HSEKAT.Models.Vehicles;

namespace HSEKAT.Services;

public class VehicleCatalog
{
    public List<VehicleType> Types { get; } = new List<VehicleType>();

    public VehicleCatalog()
    {
        Types.Add(new VehicleType("Велосипед", false, 
        (name, score, wear, mileage, _) => new Bicycle(name, score, wear, mileage)));
        
        Types.Add(new VehicleType("Электросамокат", true, 
        (name, score, wear, mileage, energy) => new Escooter(name, score, wear, mileage, energy)));
        
        Types.Add(new VehicleType("Электровелосипед", true, 
        (name, score, wear, mileage, energy) => new Ebicycle(name, score, wear, mileage, energy)));
        
        
    }
}
