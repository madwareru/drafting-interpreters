using System;
using System.Collections.Generic;
using System.Linq;
using LanguageDevShared;

namespace ReversePolishNotation;

public interface IOperation
{
    public class Put : IOperation
    {
        public readonly double Number;
        public Put(double number) => Number = number;
        public override string ToString() => $"Put({Number})";
    }

    public class Add : IOperation
    {
        public override string ToString() => "Add";
    }
    public class Div : IOperation
    {
        public override string ToString() => "Div";
    }
    public class Sqrt : IOperation
    {
        public override string ToString() => "Sqrt";
    }
}

public static class Operation
{
    public static IOperation Put(double number) => new IOperation.Put(number);
    public static readonly IOperation Add = new IOperation.Add();
    public static readonly IOperation Div = new IOperation.Div();
    public static readonly IOperation Sqrt = new IOperation.Sqrt();

    private static IResult<Stack<double>, IError> PutNumber(this Stack<double> st, double number)
    {
        st.Push(number);
        return Result.Ok<Stack<double>, IError>(st);
    }

    public static IResult<Stack<double>, IError> Eval(this IOperation operation, Stack<double> stack) =>
        operation switch
        {
            IOperation.Put put => stack.PutNumber(put.Number),
            IOperation.Add => stack.SizeAtLeast(2)
                .FlatMap(st =>
                {
                    var rhs = st.Pop();
                    var lhs = st.Pop();
                    return st.PutNumber(lhs + rhs);
                }),
            IOperation.Div => stack.SizeAtLeast(2)
                .FlatMap(st =>
                {
                    var rhs = st.Pop();
                    var lhs = st.Pop();
                    return Math.Abs(rhs) < double.Epsilon 
                        ? Result.Err<Stack<double>, IError>(Error.DivisionByZero) 
                        : st.PutNumber(lhs / rhs);
                }),
            IOperation.Sqrt => stack.SizeAtLeast(1)
                .FlatMap(st =>
                {
                    var operand = st.Pop();
                    return operand < 0.0 
                        ? Result.Err<Stack<double>, IError>(Error.SqrtOfANegativeNumber)
                        : st.PutNumber(Math.Sqrt(operand));
                }),
            _ => throw new ArgumentOutOfRangeException(nameof(operation))
        };
    
    private static IResult<Stack<double>, IError> SizeAtLeast(this Stack<double> stack, int expectedCount) => 
        stack.Count < expectedCount
            ? Result.Err<Stack<double>, IError>(Error.NotEnoughOperandsOnAStack(expectedCount, stack.Count))
            : Result.Ok<Stack<double>, IError>(stack);

    public static IResult<double, IError> GetResult(this Stack<double> stack) =>
        stack.Count >= 1
            ? Result.Ok<double, IError>(stack.Pop())
            : Result.Err<double, IError>(Error.FailedToGetResultFromAStack);

    public static IResult<double, IError> Eval(params IOperation[] operations) =>
        operations
            .Aggregate(Result.Ok<Stack<double>, IError>(new()), (current, op) => current.FlatMap(op.Eval))
            .FlatMap(GetResult);
}