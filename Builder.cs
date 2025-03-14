
/*

Builder Pattern

Explanation:
Builder is a creational design pattern that lets you construct complex objects step by step. The pattern allows you to produce different types and representations of an object using the same construction code.

Purpose:
-> Use the Builder pattern to get rid of a “telescoping constructor”.
-> Use the Builder pattern when you want your code to be able to create different representations of some product (for example, stone and wooden houses).
-> Use the Builder to construct Composite trees or other complex objects.

Pros:
-> You can construct objects step-by-step, defer construction steps or run steps recursively.
-> You can reuse the same construction code when building various representations of products.
-> Single Responsibility Principle. You can isolate complex construction code from the business logic of the product.

 */

// Code Examble

using System;

// Product: House
public class House
{
    public string Walls { get; set; }
    public string Roof { get; set; }
    public string Foundation { get; set; }

    public void ShowDetails()
    {
        Console.WriteLine($"House with {Walls} walls, {Roof} roof, and {Foundation} foundation.");
    }
}

// Builder Interface with Fluent API
public interface IHouseBuilder
{
    IHouseBuilder BuildWalls();
    IHouseBuilder BuildRoof();
    IHouseBuilder BuildFoundation();
    House GetHouse();
}

// Concrete Builder 1: Wooden House Builder
public class WoodenHouseBuilder : IHouseBuilder
{
    private House _house = new House();

    public IHouseBuilder BuildWalls()
    {
        _house.Walls = "Wooden";
        return this;
    }

    public IHouseBuilder BuildRoof()
    {
        _house.Roof = "Wooden";
        return this;
    }

    public IHouseBuilder BuildFoundation()
    {
        _house.Foundation = "Wooden Pillars";
        return this;
    }

    public House GetHouse()
    {
        House builtHouse = _house;
        _house = new House(); // Reset for next construction
        return builtHouse;
    }
}

// Concrete Builder 2: Concrete House Builder
public class ConcreteHouseBuilder : IHouseBuilder
{
    private House _house = new House();

    public IHouseBuilder BuildWalls()
    {
        _house.Walls = "Concrete";
        return this;
    }

    public IHouseBuilder BuildRoof()
    {
        _house.Roof = "Concrete";
        return this;
    }

    public IHouseBuilder BuildFoundation()
    {
        _house.Foundation = "Concrete Slab";
        return this;
    }

    public House GetHouse()
    {
        House builtHouse = _house;
        _house = new House(); // Reset for next construction
        return builtHouse;
    }
}

// Director: Controls the construction process (Optional)
public class HouseDirector
{
    private readonly IHouseBuilder _builder;

    public HouseDirector(IHouseBuilder builder)
    {
        _builder = builder;
    }

    public House ConstructHouse()
    {
        return _builder.BuildFoundation()
                       .BuildWalls()
                       .BuildRoof()
                       .GetHouse();
    }
}

// Client code
class Program
{
    static void Main(string[] args)
    {
        // Using the Director
        Console.WriteLine("Using HouseDirector:");
        HouseDirector woodenDirector = new HouseDirector(new WoodenHouseBuilder());
        House woodenHouse = woodenDirector.ConstructHouse();
        woodenHouse.ShowDetails();

        HouseDirector concreteDirector = new HouseDirector(new ConcreteHouseBuilder());
        House concreteHouse = concreteDirector.ConstructHouse();
        concreteHouse.ShowDetails();

        // Without Director (Flexible)
        Console.WriteLine("\nWithout HouseDirector:");
        IHouseBuilder customBuilder = new WoodenHouseBuilder();
        House customHouse = customBuilder.BuildWalls()
                                         .BuildFoundation()
                                         .BuildRoof()
                                         .GetHouse();
        customHouse.ShowDetails();

        // Output:
        // Using HouseDirector:
        // House with Wooden walls, Wooden roof, and Wooden Pillars foundation.
        // House with Concrete walls, Concrete roof, and Concrete Slab foundation.
        //
        // Without HouseDirector:
        // House with Wooden walls, Wooden roof, and Wooden Pillars foundation.
    }
}
