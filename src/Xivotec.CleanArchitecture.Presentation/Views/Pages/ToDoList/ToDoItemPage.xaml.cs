using Xivotec.CleanArchitecture.Presentation.ViewModels.ToDoList;

namespace Xivotec.CleanArchitecture.Presentation.Views.Pages.ToDoList;

public partial class ToDoItemPage
{
    public ToDoItemPage(ToDoItemViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}