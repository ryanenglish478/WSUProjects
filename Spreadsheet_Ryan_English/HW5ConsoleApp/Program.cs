using ExpressionTree;
using System;
using System.Collections.Generic;

namespace ExpressionTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userInput = string.Empty;
            string expression = "Not Updated";
            string variableName;
            string valueString;
            double variableValue;
            Dictionary<string, double> variables = new Dictionary<string, double>();
            while(userInput != "4")
            {
                Console.WriteLine("Menu (current expression = " + expression + ")");
                Console.WriteLine("1 = Enter a new expression");
                Console.WriteLine("2 = Set a variable value");
                Console.WriteLine("3 = Evaluate tree");
                Console.WriteLine("4 = Quit");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.Write("Enter a new Expression: ");
                        expression = Console.ReadLine();
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.Write("Enter variable name: ");
                        variableName = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Enter variable value: ");
                        valueString = Console.ReadLine();
                        if(double.TryParse(valueString, out variableValue))
                        {
                            variables.Add(variableName, variableValue);

                        }
                        else
                        {
                            Console.WriteLine("Invalid Value - Make sure it is a double type");
                        }
                        break;
                    case "3":
                        ExpressionTree tree = new ExpressionTree(expression);
                        tree.Variables = variables;
                        Console.WriteLine(tree.Evaluate());
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;

                        

                }
            }
        }
    }
}
