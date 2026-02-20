using PflanzenPI.Persistence.Business.Errors;

namespace PflanzenPI.Persistence.Business;

public record ValueResult<T, E>
    where E : class, Error
{
    private ValueResult(T? value, E? error)
    {
        Value = value;
        Error = error;
    }

    public T? Value { get; }
    public E? Error { get; }

    public bool IsSuccess => Error is null;
    public bool IsFailure => Error is not null;
    
    public ValueResult<T, U> MapError<U>(Func<E, U> mapper)
        where U : class, Error
    {
        if (IsSuccess)
            return ValueResult<T, U>.Success(Value);

        return ValueResult<T, U>.Failure(mapper(Error!));
    }


    public static ValueResult<T, E> Success(T value)
        => new(value, null);

    public static ValueResult<T, E> Failure(E error)
        => new(default, error);
}