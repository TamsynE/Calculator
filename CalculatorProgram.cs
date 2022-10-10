using System;
using System.IO;
using MyStack;
using MyQueueNew;

namespace Calculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                //> prompt;
                Console.Write(">");

                //string input = (user types);
                string equation = Console.ReadLine();

                if (equation.ToLower().Equals("exit"))
                {
                    break;
                }

                else if (equation.ToLower().Equals("quit"))
                {
                    break;
                }
                // check Parenthesis
                if (CalculatorClass.CheckParenthesis(equation) == true) // balanced
                {
                    MyQueue<string> eq = CalculatorClass.ParseEquation(equation);

                    if (eq == null)
                    {
                        Console.WriteLine($"Error: {equation} could not be parsed");
                    }

                    //Console.WriteLine(eq);
                    if (CalculatorClass.Compute(eq) != 0)
                    {
                        Console.WriteLine(CalculatorClass.Compute(eq));
                    }
                    else
                    {
                        Console.WriteLine($"Error: The values stack did not have one value remaining");
                    }
                }

                else
                {
                    Console.WriteLine($"Error: '{equation}' does not have balanced parenthesis");
                }
            }
        }
    }

    class CalculatorClass
    {

        // Verifies that a mathematical equation is written with balanced parenthesis
        // Example: "( 1 + 2 )" is balanced but "1 + 2 )" is not balanced
        static public bool CheckParenthesis(string equation)
        {
            MyStack<string> stack = new MyStack<string>();
            string open = "(";
            string close = ")";

            try
            {
                for (int i = 0; i < equation.Length; i++)
                {
                    string curr = equation.Substring(i, 1);
                    if (open.Contains(curr))
                    {
                        stack.Push(curr);
                    }
                    else if (close.Contains(curr))
                    {
                        string saved = stack.Pop();
                        if (open.IndexOf(saved) != close.IndexOf(curr))
                        {
                            return false;
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            if (stack.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Creates a queue of mathematical tokens from a mathematical string
        // Example input:  "( 1 + 2 )"
        // Example output: "(", "1", "+", "2", ")"
        // This function should also verify that there are an equal number of operators and parenthesis
        static public MyQueue<string> ParseEquation(string equation)
        {
            MyQueue<string> equationTokens = new MyQueue<string>();

            string[] tokens = equation.Split(' ');

            int countOperators = 0;
            int countParenthesis = 0;


            for (int i = 0; i < tokens.Length; i++) // create queue from array of tokens
            {
                if (tokens[i] == "+" || tokens[i] == "-" || tokens[i] == "*" || tokens[i] == "/" || tokens[i] == "**" || tokens[i] == "sqrt" || tokens[i] == "sin" || tokens[i] == "cos" || tokens[i] == "tan")
                {
                    countOperators++;
                }
                else if (tokens[i] == "(" || tokens[i] == ")")
                {
                    countParenthesis++;
                }
                equationTokens.Enqueue(tokens[i]);
            }

            if (countOperators == (countParenthesis / 2)) // check operators and parenthesis balanced
            {
                return equationTokens;
            }

            else
            {
                return null;
            }
        }

        // Calculates the result of a mathematical equation
        static public double Compute(MyQueue<string> equationTokens)
        {

            //Djikstra's Two Stack Algorithm
            MyStack<double> values = new MyStack<double>();
            MyStack<string> operands = new MyStack<string>();

            double value;
            double answer;

            foreach (string token in equationTokens)
            {
                if (double.TryParse(token, out value) == true) // if a number - add to values stack
                {
                    values.Push(value);
                }

                else if (token.Equals("sqrt") || token.Equals("sin") || token.Equals("cos") || token.Equals("tan"))
                {
                    operands.Push(token);
                }

                else if (token.Equals("+") || token.Equals("-") || token.Equals("*") || token.Equals("**") || token.Equals("/"))
                {
                    operands.Push(token);
                }

                else if (token.Equals(")"))
                {
                    string op = operands.Pop(); // so don't pop every single time

                    // if sqrt, sin, cos, tan: Pop operand and value, Push answer to values stack
                    if (op.Equals("sqrt") || op.Equals("sin") || op.Equals("cos") || op.Equals("tan"))
                    {
                        double no = values.Pop();

                        if (op.Equals("sqrt"))
                        {
                            answer = Math.Sqrt(no);
                            values.Push(answer);
                        }

                        else if (op.Equals("sin"))
                        {
                            answer = Math.Sin(no);
                            values.Push(answer);
                        }

                        else if (op.Equals("cos"))
                        {
                            answer = Math.Cos(no);
                            values.Push(answer);
                        }

                        else if (op.Equals("tan"))
                        {
                            answer = Math.Tan(no);
                            values.Push(answer);
                        }
                    }
                    // if +, -, *, /, **: pop value, operand, and value, push answer to values stack
                    else if (op.Equals("+") || op.Equals("-") || op.Equals("*") || op.Equals("/") || op.Equals("**"))
                    {
                        double no2 = values.Pop();
                        double no1 = values.Pop();

                        if (op.Equals("**")) // power
                        {
                            answer = Math.Pow(no1, no2);
                            values.Push(answer);
                        }

                        else if (op.Equals("+")) // +
                        {
                            answer = no1 + no2;
                            values.Push(answer);
                        }

                        else if (op.Equals("-")) // -
                        {
                            answer = no1 - no2;
                            values.Push(answer);
                        }

                        else if (op.Equals("*")) // *
                        {
                            answer = no1 * no2;
                            values.Push(answer);
                        }

                        else if (op.Equals("/")) // /
                        {
                            answer = no1 / no2;
                            values.Push(answer);
                        }
                    }
                }
            }

            if(values.Count == 1)
            {
                return values.Pop(); // final answer
            }
            else
            {
                return 0;
            }
        }
    }
}

