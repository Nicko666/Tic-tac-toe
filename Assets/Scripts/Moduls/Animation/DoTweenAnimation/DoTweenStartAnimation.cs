using DG.Tweening;
using UnityEngine;

public class DoTweenStartAnimation : StartAnimation
{
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] CanvasGroup _canvasGroup;

    public override void OnStart()
    {
        _canvasGroup.alpha = 0.0f;
        _rectTransform.localScale = Vector3.zero;
        _canvasGroup.DOFade(1.0f, 0.2f);
        _rectTransform.DOScale(1.0f, 0.2f);

    }


}
