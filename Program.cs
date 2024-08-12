using System.Text.RegularExpressions;

namespace CalculatorProject
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\te - Exponentiation");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|e]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        result = Calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            return;
        }
    }
}

// 2) Divide-by-zero: When the user selects the division operation ("d"),
// the program checks if the second number (num2) is zero. If it is, the result remains double.NaN to indicate an error.
// This check prevents a divide-by-zero exception.

// Unrecognized user input numbers: The program uses double.TryParse to validate the numeric
// input from the user. If the input is not a valid number, it prompts the user to enter a numeric
// value until a valid number is provided.

// Unrecognized menu options: The program expects the user to input one of the following
// operators: "a", "s", "m", or "d". If the user inputs an unrecognized operator,
// the default case in the switch statement is executed, which leaves the result as double.NaN.
// Additionally, the updated code below includes a check using regular expressions to ensure the input matches
// the expected pattern.

// Handling errors in operations: If the result of any operation is double.NaN, the program outputs a message indicating
// a mathematical error. This can occur due to division by zero or if the operator is unrecognized.

//4) Discussion on Moving User Interface and Error-Capturing Work
// Moving the user interface (UI) and error-capturing work from the Program class to the Calculator
// class is generally not a good idea for several reasons:

// Separation of Concerns: Keeping the UI logic and the business logic separate makes the code easier
// to maintain, test, and understand. The Calculator class focuses on performing calculations, while the
// Program class handles user interaction.

// Single Responsibility Principle: Each class should have a single responsibility. The Calculator class
// should only be responsible for calculations, not for handling user input or errors related to user interaction.

// Reusability: If the Calculator class handles calculations only, it can be reused in different contexts (e.g.,
// a web application, a different console application) without modification. Mixing UI logic with business logic
// reduces the reusability of the Calculator class.