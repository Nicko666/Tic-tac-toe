public class PropertyViewModel<T>
{
    protected PropertyModel<T> _model;
    public ReactiveProperty<T> property = new();


    public PropertyViewModel(PropertyModel<T> model)
    {
        if (_model != null)
            ViewModelUnsubscribe();

        _model = model;

        if (_model != null)
            ViewModelSubscribe();

        ViewModelUpdate();

    }

    void ViewModelSubscribe()
    {
        _model.property.onValueChanged += OutputProperty;
    }
    void ViewModelUnsubscribe()
    {
        _model.property.onValueChanged -= OutputProperty;
    }
    protected virtual void ViewModelUpdate()
    {
        OutputProperty(_model != null ? _model.property.Value : default);
    }

    void InoutProperty(T value)
    {
        _model.property.Value = value;
    }
    public virtual void OutputProperty(T value)
    {
        this.property.Value = value;
    }


}
