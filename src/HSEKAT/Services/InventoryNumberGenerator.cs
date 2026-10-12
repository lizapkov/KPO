namespace HSEKAT.Services;


public class InventoryNumberGenerator
{
    private int _counter = 0;

    public string NextNumber()
    {
        _counter = _counter + 1;
        return $"VK-{_counter:D4}";

    }
}
