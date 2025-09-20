using Evaluator.Core;

Console.WriteLine("Hello, Evaluator");

var infix = "1+2";
var result = EvaluatorF.Evaluate(infix);
Console.WriteLine($"{infix} = {result}");
