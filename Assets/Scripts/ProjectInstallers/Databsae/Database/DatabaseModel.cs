using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DatabaseModel<T> : ScriptableObject
{
    [SerializeField] List<T> _items;

    public List<T> Items => _items;


    //public T this[int index] //shortest way
    //{ get 
    //    { 
    //        if (!Enumerable.Range(0, _items.Count).Contains(index)) index = 0;
    //        return _items[index]; 
    //    } 
    //}
    
    public int Count => _items.Count;


    public T GetItemByIndex(int index) //common way
    {
        if (!Enumerable.Range(0, _items.Count).Contains(index)) index = 0;
        return _items[index];
    }

    public int GetIndexByItem(T item)
    {
        if (_items.Contains(item))
            return _items.IndexOf(item);

        return 0;
    }

    public T GetRandomItem()
    {
        return _items[Random.Range(0, _items.Count)];
    }

}
