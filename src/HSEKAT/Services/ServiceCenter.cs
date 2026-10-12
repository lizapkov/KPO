using HSEKAT.Interfaces;
using HSEKAT.Models;
using HSEKAT.Models.Vehicles;


namespace HSEKAT.Services;


public class ServiceCenter : IServiceCenter
{
    public const int MaxWearPercent = 50;
    public const int MaxMileage = 3000;
    public const double MaxDailyEnergyUsage = 5;

    public InspectionRes Inspect(Vehicle vehicle)
    {
        List <string> problems = new List<string>();

        if (vehicle.WearPercent > MaxWearPercent)
        {
            problems.Add($"износ равен {vehicle.WearPercent}%, допустимо не более {MaxWearPercent}%");
        }

        if (vehicle.Mileage > MaxMileage)
        {
            problems.Add($"пробег {vehicle.Mileage} км, допустимо не более {MaxMileage} км");
        }

        if (vehicle is IEnergyUser user && user.DailyEnergyUsage > MaxDailyEnergyUsage)
        {
            problems.Add($" расход энергии {user.DailyEnergyUsage} кВтч в сутки, допустимо не более {MaxDailyEnergyUsage}");
        }

        if (problems.Count > 0)
        {
            return new InspectionRes(false, string.Join("; ", problems));
        }

        return new InspectionRes(true, "техника исправна");
    }
}
