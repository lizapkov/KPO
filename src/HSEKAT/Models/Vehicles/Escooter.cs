using HSEKAT.Interfaces;

namespace HSEKAT.Models.Vehicles;

public class Escooter : Vehicle, IEnergyUser
{
    public override string NameOfType => "Электросамокат";
    public double DailyEnergyUsage { get; }

    public Escooter(string name, int score, int wearPercent, int mileage, 
    double dailyEnergyUsage) : base(name, score, wearPercent, mileage)
    {
        if (dailyEnergyUsage < 0)
        {
            throw new ArgumentException("Потребление энергии не может быть отрицательным");
        }

        DailyEnergyUsage = dailyEnergyUsage;
    }
}
