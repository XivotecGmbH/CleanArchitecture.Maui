using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Timers;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Application.Services.Time;
using Xivotec.CleanArchitecture.Presentation.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.ViewModels.Controls;

public partial class BannerViewViewModel : ViewModelBase, IRecipient<ErrorMessage>
{
    private readonly ISystemClockService _systemClock;

    [ObservableProperty]
    private string _currentDate;

    [ObservableProperty]
    private string _currentTime;

    [ObservableProperty]
    private string _errorEventMessage;

    private System.Timers.Timer _timer;

    private DateTimeOffset _time;

    public BannerViewViewModel(
        INavigationService navigation,
        ISystemClockService systemClock)
        : base(navigation)
    {
        _systemClock = systemClock;
        WeakReferenceMessenger.Default.Register(this);
        SetDateTimeTimer();
    }

    public void Receive(ErrorMessage message)
    {
        ErrorEventMessage = message.Value;
    }

    private void SetDateTimeTimer()
    {
        _timer = new(1000);
        _timer.Elapsed += TimerElapsed;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        _time = _systemClock.GetCurrentDate();
        CurrentDate = _time.ToString("dd MMMM yyyy");
        CurrentTime = _time.ToString("HH:mm:ss");
    }
}
