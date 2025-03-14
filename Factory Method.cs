/*

Factory Method Pattern

Explanation:
Factory Method is a creational design pattern that provides an interface for creating objects in a superclass, but allows subclasses to alter the type of objects that will be created.

Purpose:
-> Use the Factory Method when you don’t know beforehand the exact types and dependencies of the objects your code should work with.
-> Use the Factory Method when you want to provide users of your library or framework with a way to extend its internal components.
-> Use the Factory Method when you want to save system resources by reusing existing objects instead of rebuilding them each time.

Pros:
-> You avoid tight coupling between the creator and the concrete products.
-> Single Responsibility Principle. You can move the product creation code into one place in the program, making the code easier to support.
-> Open/Closed Principle. You can introduce new types of products into the program without breaking existing client code.

 */

// Code Example

using System;

// Step 1: Define the Product Interface
public interface IBankAccount
{
    void OpenAccount();
}

// Step 2: Implement Concrete Products
public class SavingsAccount : IBankAccount
{
    public void OpenAccount()
    {
        Console.WriteLine("Savings Account has been opened.");
    }
}

public class CurrentAccount : IBankAccount
{
    public void OpenAccount()
    {
        Console.WriteLine("Current Account has been opened.");
    }
}

// Step 3: Define the Abstract Factory
public abstract class BankAccountFactory
{
    public abstract IBankAccount CreateAccount();
}

// Step 4: Implement Concrete Factories
public class SavingsAccountFactory : BankAccountFactory
{
    public override IBankAccount CreateAccount()
    {
        return new SavingsAccount();
    }
}

public class CurrentAccountFactory : BankAccountFactory
{
    public override IBankAccount CreateAccount()
    {
        return new CurrentAccount();
    }
}

// Step 5: Client Code
class Program
{
    static void Main()
    {
        BankAccountFactory savingsFactory = new SavingsAccountFactory();
        IBankAccount savingsAccount = savingsFactory.CreateAccount();
        savingsAccount.OpenAccount();  // Output: Savings Account has been opened.

        BankAccountFactory currentFactory = new CurrentAccountFactory();
        IBankAccount currentAccount = currentFactory.CreateAccount();
        currentAccount.OpenAccount();  // Output: Current Account has been opened.
    }
}
