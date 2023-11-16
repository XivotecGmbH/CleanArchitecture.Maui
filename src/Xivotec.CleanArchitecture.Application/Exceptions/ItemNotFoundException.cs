namespace Xivotec.CleanArchitecture.Application.Exceptions;

public sealed class ItemNotFoundException : Exception
{
    public ItemNotFoundException()
    {
    }

    public ItemNotFoundException(string itemId)
        : base($"The requested Item {itemId} was not found in the repository.")
    {
    }

    public ItemNotFoundException(string itemId, Exception exception)
        : base($"The requested Item {itemId} was not found in the repository.", exception)
    {
    }
}
