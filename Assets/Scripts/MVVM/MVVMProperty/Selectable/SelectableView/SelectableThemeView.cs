using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectableThemeView : SelectableView<Theme>
{
    [SerializeField] Image _image;
    [SerializeField] TMP_Text _text;
    [SerializeField] CanvasGroup _canvasGroup;

    protected override void OutputProperty(Theme property)
    {
        _image.color = property.backgroundColor;
        _text.color = property.uiFontColor;
    }

    protected override void OutputSelected(bool value)
    {
        _canvasGroup.DOFade(value ? 1 : 0, 0.1f);
    }


}
