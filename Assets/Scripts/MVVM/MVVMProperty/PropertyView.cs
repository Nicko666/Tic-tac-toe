using UnityEngine;

public abstract class PropertyView<T> : MonoBehaviour
{
    PropertyViewModel<T> _viewModel;

    public void Init(PropertyViewModel<T> viewModel)
    {
        if (_viewModel != null)
            ViewUnsubscribe();

        _viewModel = viewModel;

        if (_viewModel != null)
            ViewSubscribe();

        ViewUpdate();

    }

    void ViewSubscribe()
    {
        _viewModel.property.onValueChanged += OutputProperty;
    }
    void ViewUnsubscribe()
    {
        _viewModel.property.onValueChanged -= OutputProperty;
    }
    void ViewUpdate()
    {
        OutputProperty(_viewModel != null ? _viewModel.property.Value : default);
    }

    public abstract void OutputProperty(T value);
    

}
