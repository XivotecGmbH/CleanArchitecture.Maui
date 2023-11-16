using Xivotec.CleanArchitecture.Presentation.ViewModels.ToDoList;

namespace Xivotec.CleanArchitecture.Presentation.Views.Pages.ToDoList;

public partial class ToDoListPage
{
    public ToDoListPage(ToDoListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}