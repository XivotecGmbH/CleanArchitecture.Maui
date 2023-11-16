using Xivotec.CleanArchitecture.Presentation.ViewModels.ToDoList;

namespace Xivotec.CleanArchitecture.Presentation.Views.Pages.ToDoList;

public partial class ToDoDetailPage
{
    public ToDoDetailPage(ToDoDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}