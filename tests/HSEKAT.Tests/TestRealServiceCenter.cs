using HSEKAT.Models.Vehicles;
using HSEKAT.Services;

namespace HSEKAT.Tests;

public class ServiceCenterTests
{
    [Theory]
    [InlineData(-1, true)]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void WearBoundary(int offset, bool expected)
    {
        var center = new ServiceCenter();
        var bike = new Bicycle("Тест", 7, ServiceCenter.MaxWearPercent + offset, 50);
        var result = center.Inspect(bike);

        Assert.Equal(expected, result.Approval);
    }

    [Theory]
    [InlineData(-1, true)]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void MileageBoundary(int offset, bool expected)
    {
        var center = new ServiceCenter();
        var bike = new Bicycle("Тест", 5, 10, ServiceCenter.MaxMileage + offset);
        var result = center.Inspect(bike);

        Assert.Equal(expected, result.Approval);
    }

    [Theory]
    [InlineData(-1, true)]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void EnergyBoundary(int offset, bool expected)
    {
        var center = new ServiceCenter();
        var scooter = new Escooter("Тест", 2, 10, 1, ServiceCenter.MaxDailyEnergyUsage + offset);
        var result = center.Inspect(scooter);

        Assert.Equal(expected, result.Approval);
    }

    [Fact]
    public void OkayVehicle_Accepted()
    {
        var center = new ServiceCenter();
        var scooter = new Escooter("Тест", 8, 20, 500, 1.5);
        var result = center.Inspect(scooter);

        Assert.True(result.Approval);
    }

    [Fact]
    public void BicycleHasNoEnergyRule()
    {
        var center = new ServiceCenter();
        var bike = new Bicycle("Тест", 8, 20, 500);
        var result = center.Inspect(bike);

        Assert.True(result.Approval);
    }

    [Fact]
    public void ThreeViolations_ReasonContainsAll()
    {
        var center = new ServiceCenter();
        var scooter = new Escooter("Тест", 8, ServiceCenter.MaxWearPercent + 1,
            ServiceCenter.MaxMileage + 1, ServiceCenter.MaxDailyEnergyUsage + 1);
        var result = center.Inspect(scooter);

        Assert.False(result.Approval);
        Assert.Contains("износ", result.Reason);
        Assert.Contains("пробег", result.Reason);
        Assert.Contains("расход", result.Reason);
    }
}
