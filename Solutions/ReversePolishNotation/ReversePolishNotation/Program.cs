// See https://aka.ms/new-console-template for more information

using ReversePolishNotation;

var testProgram = new[]
{
    Operation.Put(3), 
    Operation.Put(4), 
    Operation.Put(9), 
    Operation.Add, 
    Operation.Div, 
    Operation.Put(-7), 
    Operation.Add,
    Operation.Sqrt
};

Console.WriteLine($"[{string.Join(", ", testProgram.Select(it => it.ToString()))}]");