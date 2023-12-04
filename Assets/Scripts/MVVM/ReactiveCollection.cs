using System;
using System.Collections.ObjectModel;

public class ReactiveCollection<T>
{
    public Action<ObservableCollection<T>> onValueChanged;

    public Action<T> onAdd;
    public Action<int, int> onMove;
    public Action<T> onRemove;


    private ObservableCollection<T> _value = new();

    public ObservableCollection<T> Value
    {
        get { return _value; }
        set { _value = value; onValueChanged?.Invoke(value); }
    }

    public T this[int index]
    {
        get => Value[index];
    }

    public T Add(T item)
    {
        Value.Add(item);
        onAdd?.Invoke(item);
        return item;
    }

    public void Move(int oldIndex, int newIndex)
    {
        Value.Move(oldIndex, newIndex);
        onMove?.Invoke(oldIndex, newIndex);
    }

    public T Remove(T item)
    {
        Value.Remove(item);
        onRemove?.Invoke(item);
        return item;
    }


}
