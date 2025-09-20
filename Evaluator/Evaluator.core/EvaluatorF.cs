
namespace Evaluator.Core;

public class EvaluatorF
{
    /// <summary>
    /// This method takes an infix expression and converts it to a postfix using and then evaluates 
    /// the result with Calculate to return a double.
    /// </summary>
    /// <param name="infix"></param>
    /// <returns>Must return an double</returns>
    public static double Evaluate(string infix)
    {
        var postfix = InfixToPostfixConverter(infix);
        return Calculate(postfix);
    }
    /// <summary>
    /// Converts an infix mathematical expression to its equivalent postfix.
    /// </summary>
    /// <remarks>The method assumes that the input infix expression is well-formed and contains valid numeric
    /// values, operators, and parentheses.
    /// <param name="infix">The infix expression as a string,the expression may include numeric values, operators, and parentheses.</param>
    /// <returns>A string representing the postfix equivalent of the input infix expression.</returns>
    private static string InfixToPostfixConverter(string infix)
    {
        var stack = new Stack<char>();
        var postfix = string.Empty;
        string number = string.Empty;
        foreach (char sequence in infix)
        {
            if (char.IsDigit(sequence) || sequence == ',')
            {
                number += sequence;
            }
            else if (IsOperator(sequence))
            {
                if (number.Length > 0)
                {
                    postfix += number + " ";
                    number = "";
                }
                if (sequence == ')')
                {
                    do
                    {
                        postfix += stack.Pop() + " ";
                    } while (stack.Peek() != '(');
                    stack.Pop();
                }
                else
                {
                    if (stack.Count > 0)
                    {
                        if (PriorityInfix(sequence) > PriorityStack(stack.Peek()))
                        {
                            stack.Push(sequence);
                        }
                        else
                        {
                            postfix += stack.Pop() + " ";
                            stack.Push(sequence);
                        }
                    }
                    else
                    {
                        stack.Push(sequence);
                    }
                }
            }
        }
            if (number.Length > 0)
            {
                postfix += number + " ";
            }
            while (stack.Count > 0)
            {
                postfix += stack.Pop() + " ";
            }
            return postfix;
    }
    //Nothing to change here. ANd I also like this way of writing it.
    private static bool IsOperator(char item) => item is '^' or '/' or '*' or '%' or '+' or '-' or '(' or ')';
    //Nothing to change here. I used the teacher's code.
    private static int PriorityInfix(char op) => op switch
    {
        '^' => 4,
        '*' or '/' or '%' => 2,
        '-' or '+' => 1,
        '(' => 5,
        _ => throw new Exception("Invalid expression"),
    };
    //Nothing to change here.
    private static int PriorityStack(char op) => op switch
    {
        '^' => 3,
        '*' or '/' or '%' => 2,
        '-' or '+' => 1,
        '(' => 0,
        _ => throw new Exception("Invalid expression"),
    };
    /// <summary>
    /// Evaluates a mathematical expression in postfix notation and returns the result.
    /// </summary>
    /// <remarks>'item' disappears.Use 'sequence' instead and make the string 'number'. 
    /// Everything else works the same, except now 'sequence' is the current character you're reading.</remarks>
    /// <param name="postfix">The postfix expression to evaluate. The expression must be a valid postfix 
    /// string where operands are separated
    /// by spaces, and operators follow their operands.</param>
    /// <returns>The result of evaluating the postfix expression as a <see cref="double"/>.</returns>
    private static double Calculate(string postfix)
    {
        var stack = new Stack<double>();
        string number = string.Empty;
        foreach (char sequence in postfix)
        {
            if (char.IsDigit(sequence) || sequence == ',')
            {
                number += sequence;
            }
            else if (sequence == ' ')
            {
                if (number.Length > 0)
                {
                    stack.Push(Convert.ToDouble(number));
                    number = string.Empty;
                }
            }
            else if (IsOperator(sequence))
            {
                var op2 = stack.Pop();
                var op1 = stack.Pop();
                stack.Push(Calculate(op1, sequence, op2));
            }
        }
            if (number.Length > 0)
            {
                stack.Push(Convert.ToDouble(number));
            }
        
        return stack.Peek();
    }
    /// <summary>
    /// This method does the same thing as the teacher's code, personally I had to change nothing.
    /// </summary>
    /// <param name="op1"></param>
    /// <param name="item"></param>
    /// <param name="op2"></param>
    /// <returns></returns>
    /// <exception cref="Exception">Allows the program to perform correct calculations respecting the order of the operands</exception>
    private static double Calculate(double op1, char item, double op2) => item switch
    {
        '*' => op1 * op2,
        '/' => op1 / op2,
        '^' => Math.Pow(op1, op2),
        '+' => op1 + op2,
        '-' => op1 - op2,
        _ => throw new Exception("Invalid expression."),
    };
}
