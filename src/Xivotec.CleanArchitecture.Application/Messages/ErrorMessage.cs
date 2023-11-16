using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Xivotec.CleanArchitecture.Application.Messages;

public sealed class ErrorMessage : ValueChangedMessage<string>
{
    public ErrorMessage(string value) : base(value)
    {
    }
}
