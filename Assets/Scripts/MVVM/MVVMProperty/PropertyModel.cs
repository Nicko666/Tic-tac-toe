public class PropertyModel<T> 
{
    public ReactiveProperty<T> property = new();


    public PropertyModel(T value)
    {
        this.property.Value = value;
    }


}
