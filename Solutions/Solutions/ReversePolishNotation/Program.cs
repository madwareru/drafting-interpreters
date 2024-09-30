﻿// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
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

Console.WriteLine($"[{string.Join(", ", testProgram.Select(it => it.ToString()))}]");
Console.WriteLine($"Execution result is {Operation.Eval(testProgram)}");