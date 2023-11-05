using System;
public class Calculator
{
    public delegate double OperationDelegate(double a, double b);
    public event OperationDelegate OperationPerformed;
    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Calculator. Enter two digits and operator (+, -, *, /) through space (or enter 'cancel' to quit)");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) || input.ToLower() == "cancel")
            {
                Console.WriteLine("Calculator is ended");
                break;
            }
            string[] tokens = input.Split(' ');
            if (tokens.Length != 3)
            {
                Console.WriteLine("Invalid input format. Try again!");
            }
            double num1, num2;
            if (!double.TryParse(tokens[0], out num1) || !double.TryParse(tokens[2], out num2))
            {
                Console.WriteLine("Error parsing numbers. Try again! ");
                continue;
            }
            string op = tokens[1];
            if(OperationPerformed != null)
            {
                OperationDelegate operation = GetOperation(op);
                if(operation != null)
                {
                    double result = operation(num1, num2);
                    Console.WriteLine("Result: " + result);
                }
                else
                {
                    Console.WriteLine("Incorrect operator");
                }
            }
        }
    }
    private OperationDelegate GetOperation(string op)
    {
        switch (op)
        {
            case "+":
                return (a, b) => a + b;
            case "-":
                return (a, b) => a - b;
            case "*":
                return (a, b) => a * b;
            case "/":
                return (a, b) => a / b;
            default:
                return null;
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        calculator.OperationPerformed += (a, b) => a + b;
        calculator.Start();
    }
}