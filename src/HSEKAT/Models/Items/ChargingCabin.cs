using HSEKAT.Interfaces;

namespace HSEKAT.Models.Items;

public class ChargingCabin : Item, IEnergyUser
{
    public double DailyEnergyUsage{ get; }

    public override string NameOfType => "Зарядный шкаф";

    public ChargingCabin(string name, double dailyEnergyUsage) : base(name)
    {
        if (dailyEnergyUsage < 0)
        {
            throw new ArgumentException("Потребление энергии не может быть отрицательным");
        }

        DailyEnergyUsage = dailyEnergyUsage;
        
    }
}
