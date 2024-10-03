// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using LanguageDevShared;
using ReversePolishNotation;

var testProgram = new[]
{
    Operation.Put(256), 
    Operation.Put(7), 
    Operation.Put(9), 
    Operation.Add, 
    Operation.Div, 
    Operation.Put(-7), 
    Operation.Add,
    Operation.Sqrt
};

var testResult = Result.Ok<Stack<double>, IError>(new())
    .FlatMap(st => Operation.Put(256).Eval(st))
    .FlatMap(st => Operation.Put(7).Eval(st))
    .FlatMap(st => Operation.Put(9).Eval(st))
    .FlatMap(st => Operation.Add.Eval(st))
    .FlatMap(st => Operation.Div.Eval(st))
    .FlatMap(st => Operation.Put(-7).Eval(st))
    .FlatMap(st => Operation.Add.Eval(st))
    .FlatMap(st => Operation.Sqrt.Eval(st))
    .FlatMap(st => st.GetResult());

Console.WriteLine($"[{string.Join(", ", testProgram.Select(it => it.ToString()))}]");
Console.WriteLine($"Execution result is {testResult}");