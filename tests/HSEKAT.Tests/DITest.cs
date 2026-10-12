using HSEKAT.Interfaces;
using HSEKAT.Models.Vehicles;
using HSEKAT.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HSEKAT.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void Container_UsesFakeServiceCenter()
    {
        var fake = new FakeServiceCenter(false);

        var services = new ServiceCollection();
        services.AddSingleton<IServiceCenter>(fake);
        services.AddSingleton<InventoryNumberGenerator>();
        services.AddSingleton<Park>();
        var provider = services.BuildServiceProvider();

        var park = provider.GetRequiredService<Park>();
        var result = park.AddVehicle(new Bicycle("Тест", 5, 10, 10));

        Assert.False(result.Approval);
        Assert.Equal(1, fake.CallCount);
    }
}
