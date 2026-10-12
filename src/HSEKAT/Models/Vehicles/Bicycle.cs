namespace HSEKAT.Models.Vehicles;

public class Bicycle : Vehicle
{
    public override string NameOfType => "Велосипед";

    public Bicycle(string name, int score, int wearPercent, int mileage)
     : base(name, score, wearPercent, mileage)
    {  
    }
    
}
