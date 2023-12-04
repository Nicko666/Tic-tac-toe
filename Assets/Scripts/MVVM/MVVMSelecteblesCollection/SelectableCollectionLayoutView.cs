using System.Collections.ObjectModel;
using UnityEngine;

public class SelectableCollectionLayoutView<T> : SelectableCollectionView<T> where T : class
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] SelectableView<T> viewPrefab;
    
    ObservableCollection<SelectableView<T>> _collection = new();


    protected override void ViewSubscribe()
    {

    }

    protected override void ViewUnsubscribe()
    {
        
    }

    protected override void ViewUpdate()
    {
        foreach (var item in _collection)
            OutputRemove(item);

        foreach (var item in _viewModel.collection.Value)
            OutputAdd(item);
    }

    void OutputAdd(SelectableViewModel<T> value)
    {
        var newItem = Instantiate(viewPrefab, rectTransform);
        newItem.Init(value);
        _collection.Add(newItem);
    }
    void OutputRemove(SelectableView<T> item)
    {
        _collection.Remove(item);
        Destroy(item.gameObject);
    }


}
