using System.Collections.Generic;

public class CollectionModel<T>
{
    public ReactiveCollection<PropertyModel<T>> collection = new();


    public CollectionModel(IEnumerable<T> items)
    {
        foreach (var item in items)
            collection.Add(new (item));
    }


}
