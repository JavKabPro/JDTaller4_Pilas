
namespace Evaluator.Core;

public class EvaluatorF
{
    public static double Evaluate(string infix)
    {
        var postfix = InfixToPostfixConverter(infix);
        return Calculate(postfix);
    }
    private static string InfixToPostfixConverter(string infix)
    {
        var stack = new Stack<char>();
        var postfix = string.Empty;
        string numero = string.Empty;
        foreach (char sequence in infix)
        {
            if (char.IsDigit(sequence))
            {
                numero += sequence;
                continue;
            }
            if (IsOperator(sequence))
            {
                if (sequence == ')')
                {
                    do
                    {
                        postfix += stack.Pop();
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
                            postfix += stack.Pop();
                            stack.Push(sequence);
                        }
                    }
                    else
                    {
                        stack.Push(sequence);
                    }
                }
            }
            else
            {
                postfix += sequence;
            }
        }
        while (stack.Count > 0)
        {
            postfix += stack.Pop();
        }
        return postfix;
    }
    private static bool IsOperator(char item) => item is '^' or '/' or '*' or '%' or '+' or '-' or '(' or ')';
    private static int PriorityInfix(char op) => op switch
    {
        '^' => 4,
        '*' or '/' or '%' => 2,
        '-' or '+' => 1,
        '(' => 5,
        _ => throw new Exception("Invalid expression"),
    };
    private static int PriorityStack(char op) => op switch
    {
        '^' => 3,
        '*' or '/' or '%' => 2,
        '-' or '+' => 1,
        '(' => 0,
        _ => throw new Exception("Invalid expression"),
    };
    private static double Calculate(string postfix)
    {
        var stack = new Stack<double>();
        foreach (char sequence in postfix)
        {
            if (IsOperator(sequence))
            {
                var op2 = stack.Pop();
                var op1 = stack.Pop();
                stack.Push(Calculate(op1, sequence, op2));
            }
            else
            {
                stack.Push(Convert.ToDouble(sequence.ToString()));
            }
        }
        return stack.Peek();
    }
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
