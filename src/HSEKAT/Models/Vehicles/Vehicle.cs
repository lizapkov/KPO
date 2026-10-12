using HSEKAT.Interfaces;

namespace HSEKAT.Models.Vehicles;


public abstract class Vehicle : IInventoryItem
{
    public string InventoryName{ get; }
    public string InventoryNumber { get; set; } = string.Empty;
    public int Score { get; }
    public int WearPercent { get; }
    public int Mileage { get; }
    public abstract string NameOfType { get; }

    protected Vehicle(string name, int score, int wearPercent, int mileage)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название не может быть пустым");
        }
        
        if (score < 1 || score > 10)
        {
            throw new ArgumentException("Уровень должен быть от 1 до 10");
        }

        if (wearPercent < 0 || wearPercent > 100)
        {
            throw new ArgumentException("Износ должен быть от 0 до 100 процентов");
        }
        if (mileage < 0)
        {
            throw new ArgumentException("Пробег не может быть отрицательным");
        }

        InventoryName = name;
        Score = score;
        WearPercent = wearPercent;
        Mileage = mileage;
    }
    
}
