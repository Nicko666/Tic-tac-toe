using UnityEngine;

public abstract class SelectableCollectionView<T> : MonoBehaviour where T : class
{
    protected SelectablesCollectionViewModel<T> _viewModel;


    public void Init(SelectablesCollectionViewModel<T> viewModel)
    {
        if (_viewModel != null)
            ViewUnsubscribe();

        this._viewModel = viewModel;

        if (_viewModel != null)
            ViewSubscribe();

        ViewUpdate();

    }

    protected abstract void ViewSubscribe();
    protected abstract void ViewUnsubscribe();
    protected abstract void ViewUpdate();
    //{
        //foreach (var item in collection)
        //    OutputRemove(item);

        //foreach (var item in _viewModel.collection.Value)
        //    OutputAdd(item);
    //}


}
