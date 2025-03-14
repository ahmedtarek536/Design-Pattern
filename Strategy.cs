
/*

Strategy Pattern

Explanation:
Strategy is a behavioral design pattern that lets you define a family of algorithms, put each of them into a separate class, and make their objects interchangeable.

Purpose:
-> Use the Strategy pattern when you want to use different variants of an algorithm within an object and be able to switch from one algorithm to another during    runtime.
-> Use the Strategy when you have a lot of similar classes that only differ in the way they execute some behavior.
-> Use the pattern to isolate the business logic of a class from the implementation details of algorithms that may not be as important in the context of that logic.
-> Use the pattern when your class has a massive conditional statement that switches between different variants of the same algorithm.

Pros:
-> You can swap algorithms used inside an object at runtime.
-> You can isolate the implementation details of an algorithm from the code that uses it.
-> You can replace inheritance with composition.
-> Open/Closed Principle. You can introduce new strategies without having to change the context.

*/
using System;

// Strategy Interface
interface IPaymentStrategy
{
    void Pay(decimal amount);
}

// Concrete Strategies
class CreditCardPayment : IPaymentStrategy
{
    private readonly string _cardNumber;

    public CreditCardPayment(string cardNumber)
    {
        _cardNumber = cardNumber;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using Credit Card {_cardNumber}");
    }
}

class PayPalPayment : IPaymentStrategy
{
    private static PayPalPayment _instance;
    private readonly string _email;

    private PayPalPayment(string email) { _email = email; }

    public static PayPalPayment GetInstance(string email)
    {
        return _instance ??= new PayPalPayment(email);
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using PayPal account {_email}");
    }
}

class BitcoinPayment : IPaymentStrategy
{
    private readonly string _walletAddress;

    public BitcoinPayment(string walletAddress)
    {
        _walletAddress = walletAddress;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using Bitcoin wallet {_walletAddress}");
    }
}

// Null Object Pattern for No Payment
class NoPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("No payment method selected. Please choose one.");
    }
}

// Context Class
class ShoppingCart
{
    private IPaymentStrategy _paymentStrategy;

    public ShoppingCart(IPaymentStrategy strategy = null)
    {
        _paymentStrategy = strategy ?? new NoPayment();
    }

    public void SetPaymentMethod(IPaymentStrategy strategy)
    {
        _paymentStrategy = strategy;
    }

    public void Checkout(decimal amount)
    {
        _paymentStrategy.Pay(amount);
    }
}

// Client Code
class Program
{
    static void Main(string[] args)
    {
        // Using constructor injection (default is NoPayment)
        ShoppingCart cart = new ShoppingCart();
        cart.Checkout(100.00m); // Output: No payment method selected.

        // Switching strategies dynamically
        cart.SetPaymentMethod(new CreditCardPayment("1234-5678-9876-5432"));
        cart.Checkout(150.00m);

        cart.SetPaymentMethod(PayPalPayment.GetInstance("user@example.com"));
        cart.Checkout(80.50m);

        cart.SetPaymentMethod(new BitcoinPayment("1AaBbCcDdEeFfGg123456"));
        cart.Checkout(200.75m);
    }
}
