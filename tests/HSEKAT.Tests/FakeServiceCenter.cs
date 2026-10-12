using HSEKAT.Models;
using HSEKAT.Models.Vehicles;
using HSEKAT.Interfaces;


namespace HSEKAT.Tests;

public class FakeServiceCenter : IServiceCenter
{
    public bool Answer { get; set; }
    public int CallCount {get; private set; }
    public Vehicle? LastVehicle { get; private set; }

    public FakeServiceCenter(bool answer)
    {
        Answer = answer;
    }

    public InspectionRes Inspect(Vehicle vehicle)
    {
        CallCount++;
        LastVehicle = vehicle;
        return new InspectionRes(Answer, " ");
    }

}
