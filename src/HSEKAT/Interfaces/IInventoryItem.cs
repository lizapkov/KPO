namespace HSEKAT.Interfaces;

public interface IInventoryItem
{
    string InventoryName { get; }
    string NameOfType { get; }
    string InventoryNumber { get; set; }
}
