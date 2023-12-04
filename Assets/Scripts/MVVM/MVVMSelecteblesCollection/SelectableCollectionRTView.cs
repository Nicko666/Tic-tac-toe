using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class SelectableCollectionRTView<T> : SelectableCollectionView<T> where T : class
{
    [SerializeField] RTCollection _RTCollection;
    [SerializeField] SelectableView<T> viewPrefab;

    ObservableCollection<SelectableView<T>> _collection = new();

    [SerializeField] bool StartFromLast;


    protected override void ViewSubscribe()
    {
        _viewModel.collection.onAdd += Add;
        _viewModel.collection.onRemove += Remove;
    }
    protected override void ViewUnsubscribe()
    {
        _viewModel.collection.onAdd -= Add;
        _viewModel.collection.onRemove -= Remove;
    }
    protected override void ViewUpdate()
    {
        foreach (var item in _collection)
            Remove(item);

        foreach (var item in _viewModel.collection.Value)
            Add(item);
    }

    void Add(SelectableViewModel<T> value)
    {
        var newItem = Instantiate(viewPrefab);
        newItem.Init(value);
        _collection.Add(newItem);
        _RTCollection.Add(newItem);
        if (StartFromLast)
            _RTCollection.Move(_RTCollection.Count - 1, 0);
    }
    void Remove(SelectableViewModel<T> value)
    {
        while (_collection.Any(item => item.ViewModel == value))
        {
            Remove(_collection.First(item => item.ViewModel == value));
        }
    }
    void Remove(SelectableView<T> viewItem)
    {
        _collection.Remove(viewItem);
        _RTCollection.Remove(viewItem);
        Destroy(viewItem.gameObject);
    }


}
