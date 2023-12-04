using UnityEngine;
using UnityEngine.UI;

public abstract class PropertyImageView<T> : PropertyView<T> where T : ISprite
{
    [SerializeField] Image _image;


    public override void OutputProperty(T value)
    {
        _image.sprite = (value != null)? value.GetSprite() : null;
    }


}
