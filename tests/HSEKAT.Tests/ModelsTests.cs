using HSEKAT.Interfaces;
using HSEKAT.Models.Items;
using HSEKAT.Models.Vehicles;

namespace HSEKAT.Tests;

public class VehicleTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    public void Vehicle_InvalidScore_Throws(int score)
    {
        Assert.Throws<ArgumentException>(() => new Bicycle("Тест", score, 11, 101));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Vehicle_InvalidWear_Throws(int wear)
    {
        Assert.Throws<ArgumentException>(() => new Bicycle("Тест", 2, wear, 13));
    }

    [Fact]
    public void Vehicle_BoundaryValuesAreAllowed()
    {
        var min = new Bicycle("Тест", 1, 0, 0);
        var max = new Bicycle("Тест", 10, 100, 100);

        Assert.Equal(1, min.Score);
        Assert.Equal(10, max.Score);
    }

    [Fact]
    public void Energy_NegativeValue_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Escooter("Тест", 1, 12, 10, -1));
        Assert.Throws<ArgumentException>(() => new Ebicycle("Тест", 3, 23, 1, -1));
        Assert.Throws<ArgumentException>(() => new DockStation("Тест", -1));
        Assert.Throws<ArgumentException>(() => new ChargingCabin("Тест", -1));
    }

    [Fact]
    public void Item_EmptyName_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Helmet(""));
    }

    [Fact]
    public void OnlyElectricItemsAreEnergyConsumers()
    {
        Assert.False(new Bicycle("Тест", 2, 13, 40) is IEnergyUser);
        Assert.False(new Helmet("Тест") is IEnergyUser);
        Assert.True(new Escooter("Тест", 6, 5, 10, 1.15) is IEnergyUser);
        Assert.True(new DockStation("Тест", 1.2) is IEnergyUser);
    }

    [Fact]
    public void Items_HaveTypeNames()
    {
        Assert.Equal("Шлем", new Helmet("Тест").NameOfType);
        Assert.Equal("Док-станция", new DockStation("Тест", 1).NameOfType);
        Assert.Equal("Зарядный шкаф", new ChargingCabin("Тест", 1).NameOfType);
    }
}
