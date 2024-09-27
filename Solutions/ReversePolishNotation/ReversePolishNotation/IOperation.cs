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
}