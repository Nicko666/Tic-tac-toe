using System;
using System.Linq;
using UnityEngine;

public class SelectablesCollectionViewModel<T> where T : class
{
    CollectionModel<T> _model;

    public ReactiveCollection<SelectableViewModel<T>> collection = new();

    public Action<T> onItemSelected;


    public SelectablesCollectionViewModel(CollectionModel<T> model)
    {
        if (this._model != null)
            ViewModelUnsubscribe();

        this._model = model;

        if (this._model != null)
            ViewModelSubscribe();

        ViewModelUpdate();

    }

    public void ViewModelSubscribe()
    {
        this._model.collection.onAdd += OutputAdd;
        this._model.collection.onRemove += OutputRemove;

    }
    public void ViewModelUnsubscribe()
    {
        this._model.collection.onAdd -= OutputAdd;
        this._model.collection.onRemove -= OutputRemove;
    }
    public void ViewModelUpdate()
    {
        foreach (var item in collection.Value)
            OutputRemove(item);

        foreach (var item in _model.collection.Value)
            OutputAdd(item);

    }

    public void InputAdd(T value)
    {
        _model.collection.Add(new PropertyModel<T>(value));
    }
    public void InputRemove(T value)
    {
        var item = _model.collection.Value.First(item => item.property.Value == value);
        _model.collection.Remove(item);
    }
    public void InputItemSelect(SelectableViewModel<T> viewModel)
    {
        OutputItemSelect(viewModel);
        onItemSelected?.Invoke(viewModel.property.Value);
    }

    void OutputAdd(PropertyModel<T> value)
    {
        var item = collection.Add(new (value));
        item.onSelected += InputItemSelect;
    }
    void OutputRemove(PropertyModel<T> value)
    {
        while ( collection.Value.Any(item => item.property.Value == value.property.Value ))
        {
            OutputRemove( collection.Value.First(item => item.property.Value == value.property.Value ));
        }

    }
    void OutputRemove(SelectableViewModel<T> item)
    {
        item.onSelected -= InputItemSelect;
        collection.Remove(item);
    }
    public void OutputItemSelect(SelectableViewModel<T> value)
    {
        foreach (var item in collection.Value)
            item.OutputSelected(item == value);
    }
    public void OutputItemSelect(T value)
    {
        foreach (var item in collection.Value)
            item.OutputSelected(item.property.Value == value);
    }


}
