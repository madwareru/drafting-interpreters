namespace ReversePolishNotation;

public interface IOperation : IEquatable<IOperation>
{
    public class Put : IOperation
    {
        public readonly double Number;

        public Put(double number) => Number = number;

        public bool Equals(IOperation other) =>
            other is Put putOp &&
            Number.Equals(putOp.Number);

        public override string ToString() => $"Put({Number})";
    }
    
    public class Add : IOperation
    {
        public bool Equals(IOperation other) => other is Add;
        public override string ToString() => "Add";
    }
    
    public class Sub : IOperation
    {
        public bool Equals(IOperation other) => other is Sub;
        public override string ToString() => "Sub";
    }
    
    public class Mul : IOperation
    {
        public bool Equals(IOperation other) => other is Mul;
        public override string ToString() => "Mul";
    }
    
    public class Div : IOperation
    {
        public bool Equals(IOperation other) => other is Div;
        public override string ToString() => "Div";
    }
    
    public class Sin : IOperation
    {
        public bool Equals(IOperation other) => other is Sin;
        public override string ToString() => "Sin";
    }
    
    public class Cos : IOperation
    {
        public bool Equals(IOperation other) => other is Cos;
        public override string ToString() => "Cos";
    }
    
    public class Tan : IOperation
    {
        public bool Equals(IOperation other) => other is Tan;
        public override string ToString() => "Tan";
    }
    
    public class Sqrt : IOperation
    {
        public bool Equals(IOperation other) => other is Sqrt;
        public override string ToString() => "Sqrt";
    }
}

public static class Operation
{
    public static IOperation Put(double number) => new IOperation.Put(number);
    public static readonly IOperation Add = new IOperation.Add();
    public static readonly IOperation Sub = new IOperation.Sub();
    public static readonly IOperation Mul = new IOperation.Mul();
    public static readonly IOperation Div = new IOperation.Div();
    public static readonly IOperation Sin = new IOperation.Sin();
    public static readonly IOperation Cos = new IOperation.Cos();
    public static readonly IOperation Tan = new IOperation.Tan();
    public static readonly IOperation Sqrt = new IOperation.Sqrt();
}