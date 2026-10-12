using HSEKAT.Interfaces;

namespace HSEKAT.Models.Items;

public abstract class Item : IInventoryItem
{
    public string InventoryName { get; }
    public string InventoryNumber { get; set; } = string.Empty;
    public abstract string NameOfType { get; }

    protected Item(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя не может быть пустым");
        }
        InventoryName = name;
    }
}
