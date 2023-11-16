namespace Xivotec.CleanArchitecture.Infrastructure.Exceptions;

public sealed class InvalidPortException : Exception
{
    public InvalidPortException()
    {
    }

    public InvalidPortException(string portName)
        : base($"Loaded port {portName} is invalid or unsupported.")
    {
    }

    public InvalidPortException(string portName, Exception exception)
        : base($"Loaded port {portName} is invalid or unsupported.", exception)
    {
    }
}
