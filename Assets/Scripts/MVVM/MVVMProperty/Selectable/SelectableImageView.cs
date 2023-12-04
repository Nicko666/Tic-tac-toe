using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class SelectableImageView<T> : SelectableView<T> where T : class, ISprite
{
    [SerializeField] Image _image;
    [SerializeField] CanvasGroup _canvasGroup;


    protected override void OutputProperty(T property)
    {
        _image.sprite = property != null ? property.GetSprite() : default;
    }

    protected override void OutputSelected(bool value)
    {
        _canvasGroup.DOFade(value ? 1 : 0, 0.1f);
    }


}
