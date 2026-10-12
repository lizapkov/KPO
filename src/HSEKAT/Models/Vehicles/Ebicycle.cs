using HSEKAT.Interfaces;


namespace HSEKAT.Models.Vehicles;

public class Ebicycle : Vehicle, IEnergyUser
{
    public override string NameOfType => "Электровелосипед";

    public double DailyEnergyUsage{ get; }

    public Ebicycle(string name, int score, int wearPercent, int mileage, 
    double dailyEnergyUsage) : base(name, score, wearPercent, mileage)
    {
        if (dailyEnergyUsage < 0)
        {
            throw new ArgumentException("Потребление энергии не может быть отрицательным");
        }

        DailyEnergyUsage = dailyEnergyUsage;
    }

}
