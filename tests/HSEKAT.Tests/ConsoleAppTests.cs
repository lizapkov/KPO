using HSEKAT.Models.Vehicles;
using HSEKAT.Models.Items;
using HSEKAT.Services;


namespace HSEKAT.Tests;

[Collection("Console")]
public class ConsoleAppTests
{
    private static Park CreatePark()
    {
        return new Park(new ServiceCenter(), new InventoryNumberGenerator());
    }

    private static string RunMenu(Park park, string input)
    {
        var consoleIn = Console.In;
        var consoleOut = Console.Out;
        var output = new StringWriter();

        try
        {
            Console.SetIn(new StringReader(input));
            Console.SetOut(output);

            var app = new ConsoleApp(park, new VehicleCatalog());
            app.Run();
        }
        finally
        {
            Console.SetIn(consoleIn);
            Console.SetOut(consoleOut);
        }

        return output.ToString();
    }

    [Fact]
    public void Exit_FinishWithoutErrors()
    {
        var park = CreatePark();

        RunMenu(park, "0\n");

        Assert.Equal(0, park.VehicleCount);
    }

    [Fact]
    public void InputEnds_FinishWithoutErrors()
    {
        var park = CreatePark();

        RunMenu(park, "1\n");

        Assert.Equal(0, park.VehicleCount);
    }

    [Fact]
    public void InvalidMenuInput_AsksAgain()
    {
        var park = CreatePark();

        RunMenu(park, "cat\n9999999\n0\n");

        Assert.Equal(0, park.VehicleCount);
    }

    [Fact]
    public void AddScooter_AddedToPark()
    {
        var park = CreatePark();

        RunMenu(park, "1\n2\nТест\n2\n10\n30\n2\n0\n");

        Assert.Equal(1, park.VehicleCount);
        Assert.Equal(2.0, park.GetTotalEnergy());
    }

    [Fact]
    public void AddElectricBike_AddedToPark()
    {
        var park = CreatePark();

        RunMenu(park, "1\n3\nТест\n3\n11\n110\n1.5\n0\n");

        Assert.Equal(1, park.VehicleCount);
    }

    [Fact]
    public void AddBicycle_EmptyNameThenValid_AddedToPark()
    {
        var park = CreatePark();

        RunMenu(park, "1\n1\n\nВело\n7\n9\n30\n0\n");

        Assert.Equal(1, park.VehicleCount);
    }

    [Fact]
    public void AddScooter_TooWorn_Rejected()
    {
        var park = CreatePark();

        RunMenu(park, "1\n2\nТест\n5\n100\n450\n2\n0\n");

        Assert.Equal(0, park.VehicleCount);
    }

    [Fact]
    public void AddHelmet_AddedToPark()
    {
        var park = CreatePark();

        RunMenu(park, "2\n1\nШлем\n0\n");

        Assert.Equal(1, park.ItemCount);
    }


    [Fact]
    public void AddChargingCabin_AddedToPark()
    {
        var park = CreatePark();

        RunMenu(park, "2\n3\nШкафчик\n1\n0\n");

        Assert.Equal(1, park.ItemCount);
        Assert.Equal(1.0, park.GetTotalEnergy());
    }

    [Fact]
    public void ShowMenus_PrintInventoryNumbersAndNames()
    {
        var park = CreatePark();
        park.AddVehicle(new Escooter("Флэш", 6, 21, 432, 1.5));

        string output = RunMenu(park, "3\n4\n5\n0\n");

        Assert.Contains("VK-0001", output);
        Assert.Contains("Флэш", output);
    }

    [Fact]
    public void ShowMenus_EmptyPark_NoFail()
    {
        var park = CreatePark();

        RunMenu(park, "3\n4\n5\n0\n");

        Assert.Equal(0, park.VehicleCount);
    }

    [Fact]
    public void ShowAllStuff_PrintsTypeOfEachItem()
    {
        var park = CreatePark();
        park.AddVehicle(new Escooter("Lukmakser", 8, 20, 500, 1.5));
        park.AddItem(new Helmet("Каска"));

        string output = RunMenu(park, "5\n0\n");

        Assert.Contains("Электросамокат Lukmakser", output);
        Assert.Contains("Шлем Каска", output);
    }

}
