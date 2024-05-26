namespace DemoExamTraining2.ViewModels;

public class AddOrEditViewModel
{
    public bool IsOnEdit { get; private set; }
    
    public AddOrEditViewModel(bool isOnEdit)
    {
        IsOnEdit = isOnEdit;
    }
}