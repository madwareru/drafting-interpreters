using System;
using System.Collections.Generic;

namespace ReversePolishNotation;

public interface IOperation
{
    public void Eval(Stack<double> stack);
    
    public class Put : IOperation
    {
        public readonly double Number;
        public Put(double number) => Number = number;
        public void Eval(Stack<double> stack) => stack.Push(Number);
        public override string ToString() => $"Put({Number})";
    }

    public class Add : IOperation
    {
        public void Eval(Stack<double> stack)
        {
            var rhs = stack.Pop();
            var lhs = stack.Pop();
            stack.Push(lhs + rhs);
        }
        public override string ToString() => "Add";
    }
    public class Div : IOperation
    {
        public void Eval(Stack<double> stack)
        {
            var rhs = stack.Pop();
            var lhs = stack.Pop();
            stack.Push(Math.Abs(rhs) < double.Epsilon ? throw new DivideByZeroException() : lhs / rhs);
        }
        public override string ToString() => "Div";
    }
    public class Sqrt : IOperation
    {
        public void Eval(Stack<double> stack)
        {
            var operand = stack.Pop();
            stack.Push(operand < 0.0 ? throw new ArgumentOutOfRangeException() : Math.Sqrt(operand));
        }
        public override string ToString() => "Sqrt";
    }
}

public static class Operation
{
    public static IOperation Put(double number) => new IOperation.Put(number);
    public static readonly IOperation Add = new IOperation.Add();
    public static readonly IOperation Div = new IOperation.Div();
    public static readonly IOperation Sqrt = new IOperation.Sqrt();
    public static double Eval(params IOperation[] operations)
    {
        var stack = new Stack<double>();
        foreach (var op in operations)
            op.Eval(stack);
        return stack.Pop();
    }
}