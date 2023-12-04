using System;

public class SelectableViewModel<T> : PropertyViewModel<T>
{
    public ReactiveProperty<bool> isSelected = new();

    public Action<SelectableViewModel<T>> onSelected;


    public SelectableViewModel(PropertyModel<T> model) : base(model)
    {
        isSelected.Value = false;
    }

    public void InputSelect()
    {
        onSelected?.Invoke(this);
    }
    public void OutputSelected(bool value)
    {
        isSelected.Value = value;
    }


}
