// See https://aka.ms/new-console-template for more information

using ReversePolishNotation;

var testProgram = new[]
{
    Operation.Put(5), 
    Operation.Put(6), 
    Operation.Put(5), 
    Operation.Add, 
    Operation.Mul, 
    Operation.Put(7), 
    Operation.Sub 
};

Console.WriteLine($"[{string.Join(", ", testProgram.Select(it => it.ToString()))}]");