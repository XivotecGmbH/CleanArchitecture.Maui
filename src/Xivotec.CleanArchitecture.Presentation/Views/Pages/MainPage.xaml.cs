using Xivotec.CleanArchitecture.Presentation.ViewModels;

namespace Xivotec.CleanArchitecture.Presentation.Views.Pages;

public partial class MainPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}