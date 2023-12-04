using UnityEngine;

public abstract class SelectableView<T> : MonoBehaviour where T : class
{
    protected SelectableViewModel<T> viewModel;

    public SelectableViewModel<T> ViewModel => viewModel;


    public void Init(SelectableViewModel<T> viewModel)
    {
        if (this.viewModel != null)
            ViewUnsubscribe();

        this.viewModel = viewModel;
        
        if (this.viewModel != null)
            ViewSubscribe();

        ViewUpdate();

    }

    void ViewSubscribe()
    {
        this.viewModel.isSelected.onValueChanged += OutputSelected;

    }
    void ViewUnsubscribe()
    {
        this.viewModel.isSelected.onValueChanged -= OutputSelected;

    }
    void ViewUpdate()
    {
        if (this.viewModel != null)
        {
            OutputProperty(this.viewModel.property.Value);
            OutputSelected(this.viewModel.isSelected.Value);
        }
        else
        {
            OutputProperty(null);
            OutputSelected(false);
        }
        
    }

    protected abstract void OutputProperty(T property);
    protected abstract void OutputSelected(bool value);

    public void InputSelect()
    {
        viewModel.InputSelect();
    }
    

}
