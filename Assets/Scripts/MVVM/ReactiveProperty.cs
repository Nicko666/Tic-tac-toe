using System;

public class ReactiveProperty<T>
{
    public event Action<T> onValueChanged;

    private T _value;

    public T Value
    {
        get { return _value; }
        set { _value = value; onValueChanged?.Invoke(value); }
    }


}
