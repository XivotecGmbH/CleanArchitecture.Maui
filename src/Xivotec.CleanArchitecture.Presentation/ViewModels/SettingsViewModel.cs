using CommunityToolkit.Mvvm.ComponentModel;
using Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;
using Xivotec.CleanArchitecture.Presentation.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isLightMode;

    [ObservableProperty]
    private string _dbHost;

    [ObservableProperty]
    private string _dbPort;

    [ObservableProperty]
    private string _dbName;

    private readonly IPersistenceConfigurationService _configurationService;

    public SettingsViewModel(INavigationService navigation,
        IPersistenceConfigurationService configurationService)
        : base(navigation)
    {
        _configurationService = configurationService;

        // get current settings for display only
        var configDto = _configurationService.GetPersistenceConfigurationDto();
        DbHost = configDto.Host ?? string.Empty;
        DbPort = configDto.Port ?? string.Empty;
        DbName = configDto.PersistenceName ?? string.Empty;
    }

    partial void OnIsLightModeChanged(bool value)
    {
        Microsoft.Maui.Controls.Application.Current!.UserAppTheme =
            value ? AppTheme.Light : AppTheme.Dark;
    }
}