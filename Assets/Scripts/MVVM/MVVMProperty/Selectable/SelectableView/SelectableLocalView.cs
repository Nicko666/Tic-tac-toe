using DG.Tweening;
using TMPro;
using UnityEngine;

public class SelectableLocalView : SelectableView<Locals>
{
    [SerializeField] TMP_Text _text;
    [SerializeField] CanvasGroup _canvasGroup;

    protected override void OutputProperty(Locals property)
    {
        _text.text = property.Description;
    }

    protected override void OutputSelected(bool value)
    {
        _canvasGroup.DOFade(value ? 1 : 0, 0.1f);
    }


}
