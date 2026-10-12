using HSEKAT.Interfaces;

namespace HSEKAT.Models.Items;

public class DockStation : Item, IEnergyUser
{
    public double DailyEnergyUsage{ get; }
    public override string NameOfType => "Док-станция";

    public DockStation(string name, double dailyEnergyUsage) : base(name)
    {
        if (dailyEnergyUsage < 0)
        {
            throw new ArgumentException("Потребление энергии не может быть отрицательным");
        }

        DailyEnergyUsage = dailyEnergyUsage; 
    }
}
