using HSEKAT.Services;

namespace HSEKAT.Tests;

public class InventoryNumberGeneratorTests
{
    [Fact]
    public void Next_ReturnsFormattedNumbersInOrder()
    {
        var generator = new InventoryNumberGenerator();

        Assert.Equal("VK-0001", generator.NextNumber());
        Assert.Equal("VK-0002", generator.NextNumber());
    }

    [Fact]
    public void Next_NumbersAreUnique()
    {
        var generator = new InventoryNumberGenerator();
        var numbers = Enumerable.Range(0, 100).Select(_ => generator.NextNumber()).ToList();

        Assert.Equal(100, numbers.Distinct().Count());
    }
}
