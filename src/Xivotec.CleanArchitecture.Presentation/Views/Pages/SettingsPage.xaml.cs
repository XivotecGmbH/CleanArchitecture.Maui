using Xivotec.CleanArchitecture.Presentation.ViewModels;

namespace Xivotec.CleanArchitecture.Presentation.Views.Pages;

public partial class SettingsPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}