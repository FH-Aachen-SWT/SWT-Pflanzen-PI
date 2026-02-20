using PflanzenPI.Persistence.Business.Errors;

namespace PflanzenPI.Persistence.Business;

public abstract record BrightnessError : Error
{
    public abstract string Message { get; }
    private BrightnessError() { }

    public sealed record OwnerDoesNotExist(string owner) : BrightnessError
    {
        public override string Message => $"Owner {owner} does not exist yet";
    }

    public sealed record OwnerAlreadyExists(string owner) : BrightnessError
    {
        public override string Message => $"Owner {owner} already exists";
    }
}