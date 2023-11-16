namespace Xivotec.CleanArchitecture.Application.Exceptions;

public sealed class UnkownPortException : Exception
{
    public UnkownPortException()
    {
    }

    public UnkownPortException(string portName)
        : base($"The loaded {portName} .dll file is not recognized as a Port.")
    {
    }

    public UnkownPortException(string portName, Exception exception)
        : base($"The loaded {portName} .dll file is not recognized as a Port.", exception)
    {
    }
}
