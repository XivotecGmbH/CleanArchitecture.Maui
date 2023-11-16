using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Presentation.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    private readonly ILogger<MainViewModel> _logger;

    public MainViewModel(
        INavigationService navigation,
        ILogger<MainViewModel> logger)
        : base(navigation)
    {
        _logger = logger;

        // Logger call
        _logger.LogInformation("Main");
    }
}