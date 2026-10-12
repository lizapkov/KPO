using HSEKAT.Models;
using HSEKAT.Models.Vehicles;

namespace HSEKAT.Interfaces;

public interface IServiceCenter
{
    InspectionRes Inspect(Vehicle vehicle);
}
