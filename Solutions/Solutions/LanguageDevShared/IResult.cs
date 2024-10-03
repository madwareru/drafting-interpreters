namespace LanguageDevShared;

public interface IResult<T, TError>
{
    public class Ok : IResult<T, TError>
    {
        public readonly T Value;
        public Ok(T value) => Value = value;
        public override string ToString() => $"Ok({Value})";
    }
    public class Err : IResult<T, TError>
    {
        public readonly TError Error;
        public Err(TError error) => Error = error;
        public override string ToString() => $"Err({Error})";
    }
}

public static class Result
{
    public static IResult<T, TError> Ok<T, TError>(T value) => new IResult<T, TError>.Ok(value);
    public static IResult<T, TError> Err<T, TError>(TError error) => new IResult<T, TError>.Err(error);

    public static IResult<T1, TError> Map<T0, T1, TError>(
        this IResult<T0, TError> result, 
        Func<T0, T1> mapping
    ) => result switch
    {
        IResult<T0, TError>.Ok ok => Ok<T1, TError>(mapping(ok.Value)),
        IResult<T0, TError>.Err err => Err<T1, TError>(err.Error),
        _ => throw new ArgumentOutOfRangeException()
    };
    
    public static IResult<T1, TError> FlatMap<T0, T1, TError>(
        this IResult<T0, TError> result, 
        Func<T0, IResult<T1, TError>> mapping
    ) => result switch
    {
        IResult<T0, TError>.Ok ok => mapping(ok.Value),
        IResult<T0, TError>.Err err => Err<T1, TError>(err.Error),
        _ => throw new ArgumentOutOfRangeException()
    };
}