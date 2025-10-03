using System;
using System.Collections.Generic;

interface IVehicle
{
    void Drive();
    void Refuel();
}

class Car : IVehicle
{
    public string Brand { get; }
    public string Model { get; }
    public string FuelType { get; }

    public Car(string brand, string model, string fuelType)
    {
        Brand = brand;
        Model = model;
        FuelType = fuelType;
    }

    public void Drive()
    {
        Console.WriteLine($"Автомобиль {Brand} {Model} едет по дороге.");
    }

    public void Refuel()
    {
        Console.WriteLine($"Автомобиль {Brand} {Model} заправляется ({FuelType}).");
    }

    public override string ToString() => $"Car: {Brand} {Model}, Fuel: {FuelType}";
}

class Motorcycle : IVehicle
{
    public string Type { get; }
    public int EngineCapacity { get; }

    public Motorcycle(string type, int engineCapacity)
    {
        Type = type;
        EngineCapacity = engineCapacity;
    }

    public void Drive()
    {
        Console.WriteLine($"Мотоцикл {Type} с объемом двигателя {EngineCapacity}cc едет.");
    }

    public void Refuel()
    {
        Console.WriteLine($"Мотоцикл {Type} заправляется.");
    }

    public override string ToString() => $"Motorcycle: {Type}, Engine: {EngineCapacity}cc";
}

class Truck : IVehicle
{
    public int LoadCapacity { get; }
    public int Axles { get; }

    public Truck(int loadCapacity, int axles)
    {
        LoadCapacity = loadCapacity;
        Axles = axles;
    }

    public void Drive()
    {
        Console.WriteLine($"Грузовик с грузоподъемностью {LoadCapacity} кг и {Axles} осями в пути.");
    }

    public void Refuel()
    {
        Console.WriteLine("Грузовик заправляется дизельным топливом.");
    }

    public override string ToString() => $"Truck: {LoadCapacity} кг, Axles: {Axles}";
}

class Bus : IVehicle
{
    public int Seats { get; }
    public string Route { get; }

    public Bus(int seats, string route)
    {
        Seats = seats;
        Route = route;
    }

    public void Drive()
    {
        Console.WriteLine($"Автобус на маршруте {Route} везет {Seats} пассажиров.");
    }

    public void Refuel()
    {
        Console.WriteLine("Автобус заправляется газом.");
    }

    public override string ToString() => $"Bus: {Seats} seats, Route: {Route}";
}

abstract class VehicleFactory
{
    public abstract IVehicle CreateVehicle();
}

class CarFactory : VehicleFactory
{
    private string brand, model, fuelType;
    public CarFactory(string brand, string model, string fuelType)
    {
        this.brand = brand;
        this.model = model;
        this.fuelType = fuelType;
    }
    public override IVehicle CreateVehicle() => new Car(brand, model, fuelType);
}

class MotorcycleFactory : VehicleFactory
{
    private string type;
    private int engineCapacity;
    public MotorcycleFactory(string type, int engineCapacity)
    {
        this.type = type;
        this.engineCapacity = engineCapacity;
    }
    public override IVehicle CreateVehicle() => new Motorcycle(type, engineCapacity);
}

class TruckFactory : VehicleFactory
{
    private int loadCapacity, axles;
    public TruckFactory(int loadCapacity, int axles)
    {
        this.loadCapacity = loadCapacity;
        this.axles = axles;
    }
    public override IVehicle CreateVehicle() => new Truck(loadCapacity, axles);
}

class BusFactory : VehicleFactory
{
    private int seats;
    private string route;
    public BusFactory(int seats, string route)
    {
        this.seats = seats;
        this.route = route;
    }
    public override IVehicle CreateVehicle() => new Bus(seats, route);
}

class Program
{
    static void Main()
    {
        var vehicles = new List<IVehicle>();

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Создать автомобиль");
            Console.WriteLine("2. Создать мотоцикл");
            Console.WriteLine("3. Создать грузовик");
            Console.WriteLine("4. Создать автобус");
            Console.WriteLine("5. Показать все транспортные средства");
            Console.WriteLine("6. Выйти");
            Console.Write("Выберите действие: ");
            var choice = Console.ReadLine();

            VehicleFactory factory = null;

            switch (choice)
            {
                case "1":
                    Console.Write("Марка: ");
                    var brand = Console.ReadLine();
                    Console.Write("Модель: ");
                    var model = Console.ReadLine();
                    Console.Write("Тип топлива: ");
                    var fuel = Console.ReadLine();
                    factory = new CarFactory(brand, model, fuel);
                    break;
                case "2":
                    Console.Write("Тип мотоцикла (спортивный/туристический): ");
                    var type = Console.ReadLine();
                    Console.Write("Объем двигателя (cc): ");
                    int engine = int.Parse(Console.ReadLine());
                    factory = new MotorcycleFactory(type, engine);
                    break;
                case "3":
                    Console.Write("Грузоподъемность (кг): ");
                    int capacity = int.Parse(Console.ReadLine());
                    Console.Write("Количество осей: ");
                    int axles = int.Parse(Console.ReadLine());
                    factory = new TruckFactory(capacity, axles);
                    break;
                case "4":
                    Console.Write("Количество мест: ");
                    int seats = int.Parse(Console.ReadLine());
                    Console.Write("Маршрут: ");
                    var route = Console.ReadLine();
                    factory = new BusFactory(seats, route);
                    break;
                case "5":
                    if (vehicles.Count == 0)
                        Console.WriteLine("Нет созданных транспортных средств.");
                    else
                        vehicles.ForEach(v => Console.WriteLine(v));
                    continue;
                case "6":
                    Console.WriteLine("Выход...");
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    continue;
            }

            if (factory != null)
            {
                var vehicle = factory.CreateVehicle();
                vehicles.Add(vehicle);
                Console.WriteLine("Создано: " + vehicle);
                vehicle.Drive();
                vehicle.Refuel();
            }
        }
    }
}
