using HSEKAT.Models.Vehicles;
using HSEKAT.Models.Items;
using HSEKAT.Services;



namespace HSEKAT.Tests;

public class ParkTests
{
    [Fact]
    public void AddVehicle_ServiceCenterRejects_VehicleNotAdded()
    {
    
        var fakeCenter = new FakeServiceCenter(false);
        var park = new Park(fakeCenter, new InventoryNumberGenerator());
        var scooter = new Escooter("Тест", 5, 5, 55, 1.2);

       
        var result = park.AddVehicle(scooter);

        
        Assert.False(result.Approval);
        Assert.Equal(0, park.VehicleCount);
        Assert.Equal(string.Empty, scooter.InventoryNumber);
        Assert.Equal(1, fakeCenter.CallCount);
    }

    [Fact]
    public void AddVehicle_ServiceCenterAccepts_VehicleAddedWithNumber()
    {
        var fakeCenter = new FakeServiceCenter(true);
        var park = new Park(fakeCenter, new InventoryNumberGenerator());
        var scooter = new Escooter("Тест", 2, 99, 9999, 1.1);

        var result = park.AddVehicle(scooter);

        Assert.True(result.Approval);
        Assert.Equal(1, park.VehicleCount);
        Assert.Equal("VK-0001", scooter.InventoryNumber);
        Assert.Same(scooter, fakeCenter.LastVehicle);
    }

    [Fact]
    public void AddVehicle_RejectedVehicleDoesNotSpendNumber()
    {
        var fakeCenter = new FakeServiceCenter(false);
        var park = new Park(fakeCenter, new InventoryNumberGenerator());

        park.AddVehicle(new Bicycle("Отклонённый", 7, 10, 100));

        fakeCenter.Answer = true;
        var accepted = new Bicycle("Принятый", 7, 10, 100);
        park.AddVehicle(accepted);

        Assert.Equal("VK-0001", accepted.InventoryNumber);
    }

    private static Park CreatePark(bool accept)
    {
        return new Park(new FakeServiceCenter(accept), new InventoryNumberGenerator());
    }

    [Theory]
    [InlineData(5, false)]
    [InlineData(6, true)]
    [InlineData(7, true)]
    public void GetVehiclesForNovice_ChecksBoundary(int score, bool expected)
    {
        var park = CreatePark(true);
        park.AddVehicle(new Bicycle("Тест", score, 10, 100));

        bool isInList = park.GetVehiclesForNovice().Count == 1;

        Assert.Equal(expected, isInList);
    }

    [Fact]
    public void GetVehiclesForNovice_ReturnsOnlySuitableVehicles()
    {
        var park = CreatePark(true);
        park.AddVehicle(new Bicycle("Сложный", 4, 10, 100));
        park.AddVehicle(new Bicycle("Подходящий", 6, 10, 100));
        park.AddVehicle(new Bicycle("Простой", 9, 10, 100));

        var list = park.GetVehiclesForNovice();

        Assert.Equal(2, list.Count);
        Assert.DoesNotContain(list, vehicle => vehicle.InventoryName == "Сложный");
    }

    [Fact]
    public void GetTotalEnergy_SumsOnlyConsumers()
    {
        var park = CreatePark(true);
        park.AddVehicle(new Escooter("Самокат", 8, 10, 100, 1.5));
        park.AddVehicle(new Bicycle("Велосипед", 8, 10, 100));
        park.AddItem(new DockStation("Станция", 3.0));
        park.AddItem(new Helmet("Шлем"));

        Assert.Equal(4.5, park.GetTotalEnergy());
    }

    [Fact]
    public void GetTotalEnergy_EmptyPark_IsZero()
    {
        var park = CreatePark(true);

        Assert.Equal(0.0, park.GetTotalEnergy());
    }

    [Fact]
    public void GetTotalEnergy_RejectedVehicleIsNotCounted()
    {
        var park = CreatePark(false);
        park.AddVehicle(new Escooter("Отклонённый", 8, 10, 100, 2.0));

        Assert.Equal(0.0, park.GetTotalEnergy());
    }

    [Fact]
    public void GetAllStuff_ContainsVehiclesAndItemsWithUniqueNumbers()
    {
        var park = CreatePark(true);
        park.AddVehicle(new Bicycle("Велосипед", 8, 10, 100));
        park.AddItem(new Helmet("Шлем"));
        park.AddItem(new ChargingCabin("Шкаф", 5.0));

        var stuff = park.GetAllStuff();
        var numbers = stuff.Select(stuff => stuff.InventoryNumber).ToList();

        Assert.Equal(3, stuff.Count);
        Assert.Equal(3, numbers.Distinct().Count());
        Assert.DoesNotContain(string.Empty, numbers);
    }
}
