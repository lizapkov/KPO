using System.Text;
using HSEKAT.Services;
using Microsoft.Extensions.DependencyInjection;
using HSEKAT.Interfaces;

Console.OutputEncoding = Encoding.UTF8;

ServiceCollection services = new ServiceCollection();

services.AddSingleton<IServiceCenter, ServiceCenter>();
services.AddSingleton<InventoryNumberGenerator>();
services.AddSingleton<VehicleCatalog>();
services.AddSingleton<Park>();
services.AddSingleton<ConsoleApp>();


ServiceProvider provider = services.BuildServiceProvider();

ConsoleApp app = provider.GetRequiredService<ConsoleApp>();
if (args.Length > 0 && args[0] == "--demo")
{
    app.RunDemo();
}
else
{
    app.Run();
}
