using HSEKAT.Interfaces;
using HSEKAT.Models;
using HSEKAT.Models.Items;
using HSEKAT.Models.Vehicles;
using HSEKAT.Services;




public class ConsoleApp
{
    private readonly Park _park;
    private readonly VehicleCatalog _vehicleCatalog;

    public ConsoleApp(Park park, VehicleCatalog vehicleCatalog)
    {
        _park = park;
        _vehicleCatalog = vehicleCatalog;
    }

    public void Run()
    {
        Console.WriteLine("=== ВышКат: доедем до дедлайна ===");

        try
        {
            while (true)
            {
                PrintChoices();
                
                int choice = ReadInt("Выберите пункт из меню: ", 0, 6);
                if (choice == 0)
                {
                    break;
                }

                switch (choice)
                {
                    case 1:
                        AddVehicleFromInput();
                        break;
                    case 2:
                        AddItemFromInput();
                        break;
                    case 3:
                        ShowStatistics();
                        break;
                    case 4:
                        ShowNoviceVehicles();
                        break;
                    case 5:
                        ShowAllStuff();
                        break;
                    case 6:
                        RunDemo();
                        break;
                }
            }
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine();
            Console.WriteLine("Ввод окончен");
        }
        
    }

    public void RunDemo()
    {
        Console.WriteLine();
        Console.WriteLine("=== ДЕМКА ===");

        Console.WriteLine();
        Console.WriteLine("1. Принимаем исправный (по технической оценке) транспорт");

        Vehicle vehicle = new Ebicycle("Stanley", 8, 35, 10, 2.5);
        PrintInspection(vehicle.InventoryName, _park.AddVehicle(vehicle));

        vehicle = new Escooter("Sumsuni", 9, 10, 2, 1);
        PrintInspection(vehicle.InventoryName, _park.AddVehicle(vehicle));

        
        vehicle = new Bicycle("Oppenheimer", 3, 30, 1000);
        PrintInspection(vehicle.InventoryName, _park.AddVehicle(vehicle));

        Console.WriteLine();
        Console.WriteLine($"2. Граничные значения: износ {ServiceCenter.MaxWearPercent}%, пробег: {ServiceCenter.MaxMileage} км, расход энергии: {ServiceCenter.MaxDailyEnergyUsage} кВтч");

        vehicle = new Ebicycle("Пограничник", 5, ServiceCenter.MaxWearPercent, ServiceCenter.MaxMileage, ServiceCenter.MaxDailyEnergyUsage);
        PrintInspection(vehicle.InventoryName, _park.AddVehicle(vehicle));

        Console.WriteLine("3. Отказ в принятии транспорта по итогам техосмотра: по одному из критериев");
        
        vehicle = new Ebicycle("Veteran", 8, ServiceCenter.MaxWearPercent + 1, 100, 1.5);
        PrintInspection(vehicle.InventoryName, _park.AddVehicle(vehicle));

        vehicle = new Bicycle("Usain Bolt", 2, 40, ServiceCenter.MaxMileage + 1);
        PrintInspection(vehicle.InventoryName, _park.AddVehicle(vehicle));

        
        vehicle = new Escooter("Потребитель", 3, 30, 1000, ServiceCenter.MaxDailyEnergyUsage + 1);
        PrintInspection(vehicle.InventoryName, _park.AddVehicle(vehicle));

        Console.WriteLine();
        Console.WriteLine("4. Отказ в принятии трансопрта по итогам техосмотра: по всем критериям");
        
        vehicle = new Escooter("Последний путь", 10, ServiceCenter.MaxWearPercent + 1 , ServiceCenter.MaxMileage + 1, ServiceCenter.MaxDailyEnergyUsage + 1);
        PrintInspection(vehicle.InventoryName, _park.AddVehicle(vehicle));

        Console.WriteLine();
        Console.WriteLine("5. Ставим на баланс вещи");

        _park.AddItem(new Helmet("Hse hat"));
        _park.AddItem(new ChargingCabin("номер 1", 5.5));
        _park.AddItem(new DockStation("главный корпус", 3.5));
        Console.WriteLine("Все вещи были приняты");

        Console.WriteLine();
        Console.WriteLine("6. Количество и общее потребление энергии кВтч");
        ShowStatistics();

        Console.WriteLine();
        Console.WriteLine("7. Транспорт для новичков");
        ShowNoviceVehicles();

        Console.WriteLine();
        Console.WriteLine("8. Инвентарные номера");
        ShowAllStuff();

        Console.WriteLine();
        Console.WriteLine("=== КОНЕЦ ДЕМКИ ===");

    }

    
    private void PrintChoices()
    {
        Console.WriteLine();
        Console.WriteLine("1. Добавить транспорт (с тех осмотром)");
        Console.WriteLine("2. Добавить вещь");
        Console.WriteLine("3. Статистика: количество всего и потребление энергии");
        Console.WriteLine("4. Транспорт для начинающих");
        Console.WriteLine("5. Все наименования и номера");
        Console.WriteLine("6. Демо всех сценариев");
        Console.WriteLine("0. Выход");
    }

    private void ShowStatistics()
    {
        Console.WriteLine();
        Console.WriteLine("--- Статистика ---");
        Console.WriteLine($"Транспорт: {_park.VehicleCount}");
        Console.WriteLine($"Вещи: {_park.ItemCount}");
        Console.WriteLine($"Всего единиц: {_park.VehicleCount + _park.ItemCount}");
        Console.WriteLine($"Суммарное ежедневное потребление энергии: {_park.GetTotalEnergy()} кВтч");
    }

    private void ShowNoviceVehicles()
    {
        Console.WriteLine();
        Console.WriteLine($"--- Транспорт для новичков (уровень не ниже {Park.MinScoreForNovice}) ---");

        var list = _park.GetVehiclesForNovice();

        if (list.Count == 0)
        {
            Console.WriteLine("Отсутствует подходящий транспорт");
        }

        foreach(Vehicle vehicle in list)
        {
            Console.WriteLine($"{vehicle.InventoryNumber} | {vehicle.NameOfType} {vehicle.InventoryName} (уровень {vehicle.Score})");
        }
    }

    private void ShowAllStuff()
    {
        Console.WriteLine();
        Console.WriteLine("--- Вся техника и вещи ---");

        var stuff = _park.GetAllStuff();

        if (stuff.Count == 0)
        {
            Console.WriteLine("На балансе ничего не стоит");
        }

        foreach(IInventoryItem obj in stuff)
        {
            Console.WriteLine($"{obj.InventoryNumber} | {obj.NameOfType} {obj.InventoryName}");
        }

    }
    
    private void AddVehicleFromInput()
    {
        Console.WriteLine();
        Console.WriteLine("Виды транспорта: ");

        for (int i = 0; i < _vehicleCatalog.Types.Count; ++i)
        {
            Console.WriteLine($"{i + 1}. {_vehicleCatalog.Types[i].Name}");
        }

        int typeNumber = ReadInt("Выберите вид: ", 1, _vehicleCatalog.Types.Count);
        VehicleType type = _vehicleCatalog.Types[typeNumber - 1];

        string name = ReadText("Название: ");
        int score = ReadInt("Уровень сложности (1 - 10): ", 1, 10);
        int wear = ReadInt("Износ в процентах (0 - 100): ", 0, 100);
        int mileage = ReadInt("Пробег в км: ", 0, 10000);
        double energy = 0;

        if (type.HasEnergyUsage)
        {
            energy = ReadDouble("Ежедневный расход энергии (кВтч): ");
        
    }

    Vehicle vehicle = type.Create(name, score, wear, mileage, energy);
    InspectionRes result = _park.AddVehicle(vehicle);
    PrintInspection(vehicle.InventoryName, result);

    if (result.Approval)
        {
            Console.WriteLine($"Номер: {vehicle.InventoryNumber}");
        }
    }

    private void AddItemFromInput()
    {
        Console.WriteLine();
        Console.WriteLine("Виды вещей:");
        Console.WriteLine("1. Шлем");
        Console.WriteLine("2. Док-станция");
        Console.WriteLine("3. Зарядный шкаф");

        int type = ReadInt("Выберите вид: ",1, 3);
        string name = ReadText("Название: ");

        Item item;

        if (type == 1)
        {
            item = new Helmet(name);
        }
        else
        {
            double energy = ReadDouble("Ежедневный расход энергии (кВтч): ");

            if (type == 2)
            {
                item = new DockStation(name, energy);
            }
            else
            {
                item = new ChargingCabin(name, energy);
            }
        }
        _park.AddItem(item);
        Console.WriteLine($"Вещь поставлена на баланс в парке, идентификационный номер: {item.InventoryNumber}");

    }

    private void PrintInspection(string name, InspectionRes result)
    {
        if (result.Approval)
        {
            Console.WriteLine($"Техосмотр успешно пройден, {name} принят в парк ({result.Reason})");
        }
        else
        {
            Console.WriteLine($"Техосмотр не пройден, {name} отклонен: {result.Reason}");
        }
    } 

    private int ReadInt(string question, int min, int max)
    {
        while (true)
        {
            Console.Write(question);
            string? line = Console.ReadLine();

            if (line == null)
            {
                throw new EndOfStreamException();
            }
            if (int.TryParse(line, out int value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Пожалуйста, введите число от {min} до {max} ");
        }
    }
    private double ReadDouble(string question)
    {
        while (true)
        {
            Console.Write(question);
            string? input = Console.ReadLine();

            if (input == null)
            {
                throw new EndOfStreamException();
            }

            bool isNumber = double.TryParse(input, out double value);
            bool isNormal = isNumber && !double.IsNaN(value) && !double.IsInfinity(value);

            if (isNormal && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Пожалуйста, введите неотрицательное число");
        }
    }

    private string ReadText(string question)
    {
        while (true)
        {
            Console.Write(question);
            string? line = Console.ReadLine();

            if (line == null)
            {
                throw new EndOfStreamException();
            }

            if (!string.IsNullOrWhiteSpace(line))
            {
                return line.Trim();
            }

            Console.WriteLine("Пожалуйста, введите непустую строчку");
        }
    }
}


