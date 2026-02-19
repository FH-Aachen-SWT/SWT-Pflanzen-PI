using PflanzenPI.Persistence.Business.Errors;

namespace PflanzenPI.Persistence.Business;

public record Result<E>(E? Error) where E : class, Error
{
    public bool IsSuccess => Error == null;
    public bool IsFailure => Error != null;

    public static Result<E> Success() => new(Error: null);
    
    public static Result<E> Failure(E error) => new(error); 
    
    public Result<E> Handle(Action<E> action)
    {
        if (IsSuccess)
        {
            return Success();
        }
        action.Invoke(Error!);
        return Failure(Error!);
    }
}