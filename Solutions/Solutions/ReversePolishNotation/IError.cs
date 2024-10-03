namespace ReversePolishNotation;

public interface IError
{
    public class NotEnoughOperandsOnAStack : IError
    {
        public readonly int Expected;
        public readonly int Fact;
        public NotEnoughOperandsOnAStack(int expected, int fact)
        {
            Expected = expected;
            Fact = fact;
        }
        public override string ToString() => $"Not enough operands on a stack. Expected: {Expected}, Fact : {Fact}";
    }
    public class DivisionByZero : IError {
        public override string ToString() => "Division by zero attempt";
    }
    public class SqrtOfANegativeNumber : IError {
        public override string ToString() => "Sqrt of a negative number attempt";
    }
    public class FailedToGetResultFromAStack : IError {
        public override string ToString() => "Failed to get result from a stack";
    }
}

public static class Error
{
    public static IError NotEnoughOperandsOnAStack(int expected, int fact)
        => new IError.NotEnoughOperandsOnAStack(expected, fact);
    public static readonly IError DivisionByZero = new IError.DivisionByZero();
    public static readonly IError SqrtOfANegativeNumber = new IError.SqrtOfANegativeNumber();
    public static readonly IError FailedToGetResultFromAStack = new IError.FailedToGetResultFromAStack();
}